using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapitalMarketData.Data.Repositories;

public class InstrumentRepository : IInstrumentRepository
{
    private readonly CapitalMarketDataDbContext _db;

    public InstrumentRepository(CapitalMarketDataDbContext db)
    {
        _db = db;
    }

    public async Task<int> AddInstrument(Instrument instrument)
    {
        ArgumentNullException.ThrowIfNull(instrument);

        int affected = 0;
        if (await _db.Instruments.AnyAsync(x => x.Id == instrument.Id) == false)
        {
            await _db.Instruments.AddAsync(instrument);
            affected = _db.SaveChanges();
        }
        return affected;
    }

    public async Task<IEnumerable<Instrument>> GetAll()
    {
        return await _db.Instruments.ToListAsync();
    }

    public async Task<Instrument?> GetInstrumentById(string id)
    {
        return await _db.Instruments.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Instrument?> GetInstrumentByTicker(string ticker)
    {
        return await _db.Instruments.FirstOrDefaultAsync(x => x.Ticker == ticker);
    }
}
