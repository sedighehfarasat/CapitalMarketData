using AutoMapper;
using CapitalMarketData.Entities.DTOs;
using CapitalMarketData.Entities.Entities;
using CapitalMarketData.Entities.Enums;

namespace CapitalMarketData.WebAPI.MappingProfiles;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<Stock, StockViewModel>()
            .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
            .ForMember(d => d.InsCode, mo => mo.MapFrom(s => s.InsCode))
            .ForMember(d => d.Ticker, mo => mo.MapFrom(s => s.Ticker))
            .ForMember(d => d.Name, mo => mo.MapFrom(s => s.Name))
            .ForMember(d => d.Board, mo => mo.MapFrom(s => s.Board))
            .ForMember(d => d.Industry, mo => mo.MapFrom(s => Enum.GetName(typeof(Sector), s.Sector)));
    }
}
