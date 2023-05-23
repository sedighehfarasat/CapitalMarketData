using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapitalMarketData.Data.Repositories;

public class StockRepository : IStockRepository
{
    private readonly CapitalMarketDataDbContext _db;

    public StockRepository(CapitalMarketDataDbContext db)
    {
        _db = db;
    }

    public async Task<int> AddInstrument(Stock stock)
    {
        ArgumentNullException.ThrowIfNull(stock);

        int affected = 0;
        if (await _db.Stocks.AnyAsync(x => x.Id == stock.Id) == false)
        {
            await _db.Stocks.AddAsync(stock);
            affected = _db.SaveChanges();
        }
        return affected;
    }

    public async Task<IEnumerable<Stock>> GetAll()
    {
        return await _db.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetInstrumentById(string id)
    {
        return await _db.Stocks.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Stock?> GetInstrumentByTicker(string ticker)
    {
        return await _db.Stocks.FirstOrDefaultAsync(x => x.Ticker == ticker);
    }
}
