using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Entities.Contracts;

public interface IStockRepository
{
    Task<IEnumerable<Stock>> GetAll();
    Task<Stock?> GetById(string id);
    Task<Stock?> GetByTicker(string ticker);
    Task<int> Add(Stock stock);
    Task Update(Stock stock);
}
