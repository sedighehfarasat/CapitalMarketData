using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Worker.Services;

public class InstiIndiService
{
    private readonly IIndiInstiTradingDataRepository _indiInstiDataRepo;

    public InstiIndiService(IIndiInstiTradingDataRepository indiInstiDataRepo)
    {
        _indiInstiDataRepo = indiInstiDataRepo;
    }

    public async Task Add(Instrument instrument)
    {
        ArgumentNullException.ThrowIfNull(instrument);
        ArgumentNullException.ThrowIfNull(instrument.InsCode);

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
    }
}
