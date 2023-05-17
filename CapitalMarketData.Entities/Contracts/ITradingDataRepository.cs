using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Entities.Contracts;

public interface ITradingDataRepository
{
    Task<TradingData?> GetTodayTradingDataByInstrumentId(string instrumentId);
    Task<int> AddTradingData(TradingData? data);
}
