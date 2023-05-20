using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Entities.Contracts;

public interface IInstrumentRepository
{
    Task<IEnumerable<Instrument>> GetAll();
    Task<Instrument?> GetInstrumentById(string id);
    Task<Instrument?> GetInstrumentByTicker(string ticker);
    Task<int> AddInstrument(Instrument instrument);
}
