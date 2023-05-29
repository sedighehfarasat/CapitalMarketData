using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Entities.Contracts;

public interface INavRepository
{
    Task<NAV?> GetTodayNAVByInstrumentId(string instrumentId);
    Task Add(NAV? nav);
}
