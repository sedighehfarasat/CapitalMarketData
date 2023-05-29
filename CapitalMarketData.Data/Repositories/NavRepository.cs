using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapitalMarketData.Data.Repositories;

public class NavRepository : INavRepository
{
    private readonly CapitalMarketDataDbContext _db;

    public NavRepository(CapitalMarketDataDbContext db)
    {
        _db = db;
    }

    public async Task Add(NAV? nav)
    {
        ArgumentNullException.ThrowIfNull(nav);

        if (await _db.NAVs.AnyAsync(x => x.Date == nav.Date && x.InstrumentId == nav.InstrumentId) == false)
        {
            await _db.NAVs.AddAsync(nav);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<NAV?> GetTodayNAVByInstrumentId(string instrumentId)
    {
        return await _db.NAVs.FirstOrDefaultAsync(x => x.Date.Date == DateTime.Today && x.InstrumentId == instrumentId);
    }
}
