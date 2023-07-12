using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using CapitalMarketData.Worker.Services;
using Serilog;

namespace CapitalMarketData.BackgroundWorker;

public class Worker : BackgroundService
{
    private readonly IInstrumentRepository _instrumentRepo;
    private readonly InstrumentsService _instrumentsService;
    private readonly TradingDataService _tradingDataService;
    private readonly InstiIndiService _instiIndiService;

    public Worker(IInstrumentRepository instrumentRepo, InstrumentsService instrumentsService, TradingDataService tradingDataService, InstiIndiService instiIndiService)
    {
        _instrumentRepo = instrumentRepo;
        _instrumentsService = instrumentsService;
        _tradingDataService = tradingDataService;
        _instiIndiService = instiIndiService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Do You Want To Update The List Of Instuments? Y/N");
        var isUpdateNeeded = Console.ReadLine()!;

        #region Updating Instruments
        if (isUpdateNeeded.ToLower() == "y")
        {
            Log.Information("The List Of Instuments Is Updating ...");
            await _instrumentsService.Update();
            Log.Information("The List Of Instuments Updated.");
        }
        #endregion

        List<Instrument> Instruments = await _instrumentRepo.GetAll();
        if (!Instruments.Any())
        {
            Log.Error($"No Instrument Found!");
            await StopAsync(stoppingToken);
        }

        foreach (var instrument in Instruments)
        {
            try
            {
                #region Adding Trading Data
                await _tradingDataService.Add(instrument);
                Log.Information($"Trading Data Added For {instrument.Id}.");
                #endregion

                #region Add InstiIndi Trading Data
                await _instiIndiService.Add(instrument);
                Log.Information($"Individual And Institutional Trading Data Added For {instrument.Id}.");
                #endregion
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
