using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapitalMarketData.Data.Repositories;

public class IndiInstiTradingDataRepository : IIndiInstiTradingDataRepository
{
    private readonly CapitalMarketDataDbContext _db;

    public IndiInstiTradingDataRepository(CapitalMarketDataDbContext db)
    {
        _db = db;
    }

    public async Task Add(IndiInstiTradingData? data)
    {
        ArgumentNullException.ThrowIfNull(data);

        if (await _db.IndiInstiTradingData.AnyAsync(x => x.Date == data.Date && x.InstrumentId == data.InstrumentId) == false)
        {
            await _db.IndiInstiTradingData.AddAsync(data);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<IndiInstiTradingData?> GetTodayDataByInstrumentId(string instrumentId)
    {
        return await _db.IndiInstiTradingData.FirstOrDefaultAsync(x => x.Date.Date == DateTime.Today && x.InstrumentId == instrumentId);
    }
}
