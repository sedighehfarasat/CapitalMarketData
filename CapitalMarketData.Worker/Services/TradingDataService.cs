using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using CapitalMarketData.Worker.Helper;

namespace CapitalMarketData.Worker.Services;

public class TradingDataService
{
    private readonly ITradingDataRepository _tradingDataRepo;

    public TradingDataService(ITradingDataRepository tradingDataRepo)
    {
        _tradingDataRepo = tradingDataRepo;
    }

    public async Task Add(Instrument instrument)
    {
        ArgumentNullException.ThrowIfNull(instrument);
        ArgumentNullException.ThrowIfNull(instrument.InsCode);

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
    }
}
