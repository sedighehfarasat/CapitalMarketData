using AutoMapper;
using CapitalMarketData.Entities.DTOs;
using CapitalMarketData.Entities.Entities;
using CapitalMarketData.Entities.Enums;

namespace CapitalMarketData.WebAPI.MappingProfiles;

public class InstrumentProfile : Profile
{
    public InstrumentProfile()
    {
        CreateMap<Instrument, InstrumentViewModel>()
            .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
            .ForMember(d => d.InsCode, mo => mo.MapFrom(s => s.InsCode))
            .ForMember(d => d.Ticker, mo => mo.MapFrom(s => s.Ticker))
            .ForMember(d => d.Name, mo => mo.MapFrom(s => s.Name))
            .ForMember(d => d.Type, mo => mo.MapFrom(s => Enum.GetName(typeof(InstrumentType), s.Type)))
            .ForMember(d => d.Sector, mo => mo.MapFrom(s => Enum.GetName(typeof(Sector), s.Sector)));
    }
}
