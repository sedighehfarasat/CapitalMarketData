using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using CapitalMarketData.Entities.Enums;
using CapitalMarketData.Worker.Helper;
using CapitalMarketData.Worker.Services;
using Serilog;
using System.Linq;

namespace CapitalMarketData.BackgroundWorker;

public class Worker : BackgroundService
{
    private readonly IStockRepository _stockRepo;
    private readonly IEtfRepository _etfRepo;
    private readonly ITradingDataRepository _tradingDataRepo;
    private readonly UpdateInstruments _updateInstruments;

    public Worker(IStockRepository stockRepo, IEtfRepository etfRepo, ITradingDataRepository tradingDataRepo, UpdateInstruments updateInstruments)
    {
        _stockRepo = stockRepo;
        _etfRepo = etfRepo;
        _tradingDataRepo = tradingDataRepo;
        _updateInstruments = updateInstruments;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Do You Want To Update The List Of Instuments? Y/N");
        var isUpdateNeeded = Console.ReadLine()!;

        #region Updating Instruments
        if (isUpdateNeeded.ToLower() == "y")
        {
            Log.Information("The List Of Instuments Is Updating ...");
            await _updateInstruments.Update();
            Log.Information("The List Of Instuments Updated.");
        }
        #endregion

        List<Stock> stocks = await _stockRepo.GetAll();
        List<Instrument> convertedStocks = stocks.ConvertAll(x => x as Instrument);
        List<ETF> etfs = await _etfRepo.GetAll();
        List<Instrument> convertedEtfs = etfs.ConvertAll(x => x as Instrument);
        IEnumerable<Instrument> Instruments = convertedStocks.Union(convertedEtfs);
        if (!Instruments.Any())
        {
            Log.Error($"No Instrument Found!");
            await StopAsync(stoppingToken);
        }

        foreach (var instrument in Instruments)
        {
            if (instrument.Id is not null)
            {
                try
                {
                    var data = await TseService.FetchLiveData(instrument.Id);
                    if (data is not null)
                    {
                        TradingData tradingData = new()
                        {
                            InstrumentId = instrument.Id,
                            Status = Convertor.ToStatusEnum(data.header[0].state),
                        };
                        if (tradingData.Status == Status.Trading)
                        {
                            tradingData.OpeningPrice = decimal.Parse(data.mainData.agh);
                            tradingData.HighestPrice = decimal.Parse(data.mainData.bt.u);
                            tradingData.LowestPrice = decimal.Parse(data.mainData.bt.d);
                            tradingData.LastPrice = decimal.Parse(data.header[1].am);
                            tradingData.ClosingPrice = decimal.Parse(data.mainData.ghp.v);
                            tradingData.PreviousClosingPrice = decimal.Parse(data.mainData.rgh);
                            tradingData.UpperBoundPrice = decimal.Parse(data.mainData.bm.u);
                            tradingData.LowerBoundPrice = decimal.Parse(data.mainData.bm.d);
                            tradingData.NumberOfTrades = int.Parse(data.mainData.dm.Replace(",", string.Empty));
                            tradingData.TradingValue = Convertor.ToNumber(data.mainData.arm);
                            tradingData.TradingVolume = (long?)Convertor.ToNumber(data.mainData.hmo);
                        }
                        int affected = await _tradingDataRepo.Add(tradingData);
                        Log.Information($"{affected} row affected for {instrument.Id}");
                    }
                }
                catch (HttpRequestException e)
                {
                    Log.Error($"Http Exception Caught On {instrument.Id}: {e.Message}");
                }
                catch (Exception e)
                {
                    Log.Error($"Other Exception Caught On {instrument.Id}: {e.Message}");
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
