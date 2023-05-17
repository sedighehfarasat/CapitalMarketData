using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using CapitalMarketData.Entities.Enums;
using CapitalMarketData.Worker.Helper;
using CapitalMarketData.Worker.Services;
using Serilog;

namespace CapitalMarketData.BackgroundWorker;

public class Worker : BackgroundService
{
    private readonly IInstrumentRepository _instrumentRepo;
    private readonly ITradingDataRepository _tradingDataRepo;

    public Worker(IInstrumentRepository instrumentRepo, ITradingDataRepository tradingDataRepo)
    {
        _instrumentRepo = instrumentRepo;
        _tradingDataRepo = tradingDataRepo;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var instruments = await _instrumentRepo.GetAll();
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
                    var data = TseService.FetchLiveData(instrument.Id);
                    if (data.Result is not null)
                    {
                        TradingData tradingData = new()
                        {
                            InstrumentId = instrument.Id,
                            Status = Convertor.ToStatusEnum(data.Result.header[0].state),
                        };
                        if (tradingData.Status == Status.Trading)
                        {
                            tradingData.OpeningPrice = decimal.Parse(data.Result.mainData.agh);
                            tradingData.HighestPrice = decimal.Parse(data.Result.mainData.bt.u);
                            tradingData.LowestPrice = decimal.Parse(data.Result.mainData.bt.d);
                            tradingData.LastPrice = decimal.Parse(data.Result.header[1].am);
                            tradingData.ClosingPrice = decimal.Parse(data.Result.mainData.ghp.v);
                            tradingData.PreviousClosingPrice = decimal.Parse(data.Result.mainData.rgh);
                            tradingData.UpperBoundPrice = decimal.Parse(data.Result.mainData.bm.u);
                            tradingData.LowerBoundPrice = decimal.Parse(data.Result.mainData.bm.d);
                            tradingData.NumberOfTrades = int.Parse(data.Result.mainData.dm.Replace(",", string.Empty));
                            tradingData.TradingValue = Convertor.ToNumber(data.Result.mainData.arm);
                            tradingData.TradingVolume = (long?)Convertor.ToNumber(data.Result.mainData.hmo);
                        }
                        int affected = await _tradingDataRepo.AddTradingData(tradingData);
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
