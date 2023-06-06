using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using CapitalMarketData.Worker.Helper;
using CapitalMarketData.Worker.Services;
using Serilog;

namespace CapitalMarketData.BackgroundWorker;

public class Worker : BackgroundService
{
    private readonly IInstrumentRepository _instrumentRepo;
    private readonly ITradingDataRepository _tradingDataRepo;
    private readonly IIndiInstiTradingDataRepository _indiInstiDataRepo;
    private readonly InstrumentsService _instrumentsService;

    public Worker(IInstrumentRepository instrumentRepo, ITradingDataRepository tradingDataRepo, IIndiInstiTradingDataRepository indiInstiDataRepo, InstrumentsService instrumentsService)
    {
        _instrumentRepo = instrumentRepo;
        _tradingDataRepo = tradingDataRepo;
        _indiInstiDataRepo = indiInstiDataRepo;
        _instrumentsService = instrumentsService;
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
            if (instrument.Id is not null && instrument.InsCode is not null)
            {
                try
                {
                    #region Adding Trading Data
                    var priceData = await TsetmcService.GetPriceData(instrument.InsCode);
                    var stateData = await TsetmcService.GetInstrumentState(instrument.InsCode);
                    var staticThresholdData = await TsetmcService.GetStaticThresholds(instrument.InsCode);
                    if (priceData is not null && stateData is not null && staticThresholdData is not null)
                    {
                        TradingData tradingData = new()
                        {
                            InstrumentId = instrument.Id,
                            Status = Convertor.ToStatusEnum(stateData.cEtaval),
                            OpeningPrice = (decimal)priceData.closingPriceDaily.priceFirst,
                            HighestPrice = (decimal)priceData.closingPriceDaily.priceMax,
                            LowestPrice = (decimal)priceData.closingPriceDaily.priceMin,
                            LastPrice = (decimal)priceData.closingPriceDaily.pDrCotVal,
                            ClosingPrice = (decimal)priceData.closingPriceDaily.pClosing,
                            PreviousClosingPrice = (decimal)priceData.closingPriceDaily.priceYesterday,
                            UpperBoundPrice = (decimal)staticThresholdData.psGelStaMax,
                            LowerBoundPrice = (decimal)staticThresholdData.psGelStaMin,
                            NumberOfTrades = (int)priceData.closingPriceDaily.zTotTran,
                            TradingValue = (decimal)priceData.closingPriceDaily.qTotCap,
                            TradingVolume = (long)priceData.closingPriceDaily.qTotTran5J,
                        };

                        await _tradingDataRepo.Add(tradingData);
                    }

                    Log.Information($"Trading Data Added For {instrument.Id}.");
                    #endregion

                    #region Add InstiIndi Trading Data
                    var clientTypeData = await TsetmcService.GetInstitutionalIndividualData(instrument.InsCode);
                    if (clientTypeData is not null)
                    {
                        IndiInstiTradingData indiInstiData = new()
                        {
                            InstrumentId = instrument.Id,
                            IndividualNumberOfTrades_BuySide = clientTypeData.clientType.buy_CountI,
                            IndividualNumberOfTrades_SellSide = clientTypeData.clientType.sell_CountI,
                            IndividualTradingVolume_BuySide = (long)clientTypeData.clientType.buy_I_Volume,
                            IndividualTradingVolume_SellSide = (long)clientTypeData.clientType.sell_I_Volume,
                            InstitutionalNumberOfTrades_BuySide = clientTypeData.clientType.buy_CountN,
                            InstitutionalNumberOfTrades_SellSide = clientTypeData.clientType.sell_CountN,
                            InstitutionalTradingVolume_BuySide = (long)clientTypeData.clientType.buy_N_Volume,
                            InstitutionalTradingVolume_SellSide = (long)clientTypeData.clientType.sell_N_Volume,
                        };

                        await _indiInstiDataRepo.Add(indiInstiData);
                    }

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
}
