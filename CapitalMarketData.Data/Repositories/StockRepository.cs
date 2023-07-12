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

    public async Task<List<Stock>> GetAll()
    {
        return await _db.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetById(string id)
    {
        return await _db.Stocks.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Stock?> GetByTicker(string ticker)
    {
        return await _db.Stocks.FirstOrDefaultAsync(x => x.Ticker == ticker);
    }
}
