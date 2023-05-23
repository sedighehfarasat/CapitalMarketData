using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Entities.Contracts;

public interface IStockRepository
{
    Task<IEnumerable<Stock>> GetAll();
    Task<Stock?> GetInstrumentById(string id);
    Task<Stock?> GetInstrumentByTicker(string ticker);
    Task<int> AddInstrument(Stock instrument);
}
