using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Entities.Contracts;

public interface IInstrumentRepository
{
    Task<List<Instrument>> GetAll();
    //Task<Instrument?> GetById(string id);
    //Task<Instrument?> GetByTicker(string ticker);
    Task Add(Instrument instrument);
    Task Update(Instrument instrument);
}
