using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapitalMarketData.Data.Repositories;

public class TradingDataRepository : ITradingDataRepository
{
    private readonly CapitalMarketDataDbContext _db;

    public TradingDataRepository(CapitalMarketDataDbContext db)
    {
        _db = db;
    }

    public async Task Add(TradingData? data)
    {
        ArgumentNullException.ThrowIfNull(data);

        if (await _db.TradingData.AnyAsync(x => x.Date == data.Date && x.InstrumentId == data.InstrumentId) == false)
        {
            await _db.TradingData.AddAsync(data);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<TradingData?> GetTodayDataByInstrumentId(string instrumentId)
    {
        return await _db.TradingData.FirstOrDefaultAsync(x => x.Date.Date == DateTime.Today && x.InstrumentId == instrumentId);
    }
}
