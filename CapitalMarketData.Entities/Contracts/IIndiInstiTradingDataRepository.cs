using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Entities.Contracts;

public interface IIndiInstiTradingDataRepository
{
    Task<IndiInstiTradingData?> GetTodayDataByInstrumentId(string instrumentId);
    Task Add(IndiInstiTradingData? data);
}
