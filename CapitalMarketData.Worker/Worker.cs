using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using CapitalMarketData.Entities.Enums;
using CapitalMarketData.Worker.Helper;
using CapitalMarketData.Worker.Models;
using CapitalMarketData.Worker.Services;
using Serilog;

namespace CapitalMarketData.BackgroundWorker;

public class Worker : BackgroundService
{
    private readonly IStockRepository _stockRepo;
    private readonly ITradingDataRepository _tradingDataRepo;

    public Worker(IStockRepository stockRepo, ITradingDataRepository tradingDataRepo)
    {
        _stockRepo = stockRepo;
        _tradingDataRepo = tradingDataRepo;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Do You Want To Update Instument List? Y/N");
        var isUpdateNeeded = Console.ReadLine()!;

        #region Updating Instruments
        if (isUpdateNeeded.ToLower() == "y")
        {
            Companies? companies = await TseService.GetStockList();
            if (companies is null)
            {
                Log.Error($"No List Of Companies!");
                Environment.Exit(1);
            }

            foreach (var category in companies.companies)
            {
                foreach (var instrument in category.list)
                {
                    var stock = new Entities.Entities.Stock()
                    {
                        Id = instrument.ic,
                        Ticker = instrument.sy,
                        Name = instrument.n,
                    };

                    int affected = await _stockRepo.Add(stock);
                    Log.Information($"{affected} row affected for {stock.Id}");
                }
            }

            var insCodes = await TsetmcService.GetInsCodesFromFile();
            foreach (var code in insCodes)
            {
                try
                {
                    var data = await TsetmcService.GetIntrumentInfo(code);
                    if (data is not null)
                    {
                        var instrument = await _stockRepo.GetById(data.instrumentIdentity.instrumentID);
                        if (instrument is not null)
                        {
                            instrument.InsCode = code;
                            //instrument.Board = ;
                            instrument.Industry = (Industry)int.Parse(data.instrumentIdentity.sector.cSecVal.Trim());
                            await _stockRepo.Update(instrument);
                        }
                    }
                }
                catch (HttpRequestException)
                {
                    Log.Information($"There is no instrument with this INS code: {code}");
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
        #endregion

        var instruments = await _stockRepo.GetAll();
        if (!instruments.Any())
        {
            Log.Error($"No Instrument Found!");
            await StopAsync(stoppingToken);
        }

        foreach (var instrument in instruments)
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

                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
