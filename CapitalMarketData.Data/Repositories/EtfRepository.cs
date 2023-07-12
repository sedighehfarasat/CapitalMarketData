using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapitalMarketData.Data.Repositories;

public class EtfRepository : IEtfRepository
{
    private readonly CapitalMarketDataDbContext _db;

    public EtfRepository(CapitalMarketDataDbContext db)
    {
        _db = db;
    }

    public async Task<List<ETF>> GetAll()
    {
        return await _db.ETFs.ToListAsync();
    }

    public async Task<ETF?> GetById(string id)
    {
        return await _db.ETFs.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ETF?> GetByTicker(string ticker)
    {
        return await _db.ETFs.FirstOrDefaultAsync(x => x.Ticker == ticker);
    }
}
