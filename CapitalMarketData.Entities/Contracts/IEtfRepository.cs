using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Entities.Contracts;

public interface IEtfRepository
{
    Task<IEnumerable<ETF>> GetAll();
    Task<ETF?> GetById(string id);
    Task<ETF?> GetByTicker(string ticker);
    Task Add(ETF etf);
    Task Update(ETF etf);
}
