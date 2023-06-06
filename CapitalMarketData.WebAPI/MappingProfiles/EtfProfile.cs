using AutoMapper;
using CapitalMarketData.Entities.DTOs;
using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.WebAPI.MappingProfiles;

public class EtfProfile : Profile
{
    public EtfProfile()
    {
        CreateMap<ETF, EtfViewModel>()
            .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
            .ForMember(d => d.InsCode, mo => mo.MapFrom(s => s.InsCode))
            .ForMember(d => d.Ticker, mo => mo.MapFrom(s => s.Ticker))
            .ForMember(d => d.Name, mo => mo.MapFrom(s => s.Name))
            .ForMember(d => d.Type, mo => mo.MapFrom(s => s.Type))
            .ForMember(d => d.Subsector, mo => mo.MapFrom(s => s.Subsector));
    }
}
