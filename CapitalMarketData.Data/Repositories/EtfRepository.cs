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

    public async Task Add(ETF etf)
    {
        ArgumentNullException.ThrowIfNull(etf);

        if (await _db.ETFs.AnyAsync(x => x.Id == etf.Id) == false)
        {
            await _db.ETFs.AddAsync(etf);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ETF>> GetAll()
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

    public async Task Update(ETF etf)
    {
        _db.Entry(etf).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }
}
