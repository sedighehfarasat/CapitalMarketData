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

    public async Task Add(Instrument instrument)
    {
        ArgumentNullException.ThrowIfNull(instrument);

        if (await _db.Instruments.AnyAsync(x => x.Id == instrument.Id) == false)
        {
            await _db.Instruments.AddAsync(instrument);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<List<Instrument>> GetAll()
    {
        return await _db.Instruments.ToListAsync();
    }

    public async Task<Instrument?> GetById(string id)
    {
        return await _db.Instruments.FirstOrDefaultAsync(x => x.Id == id);
    }

    //public async Task<Instrument?> GetByTicker(string ticker)
    //{
    //    return await _db.Instruments.FirstOrDefaultAsync(x => x.Ticker == ticker);
    //}

    public async Task Update(Instrument instrument)
    {
        _db.Entry(instrument).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }
}
