using AutoMapper;
using CapitalMarketData.Entities.DTOs;
using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.WebAPI.MappingProfiles;

public class TradingDataProfile : Profile
{
    public TradingDataProfile()
    {
        CreateMap<TradingData, TradingDataViewModel>()
            .ForMember(d => d.InstrumentId, mo => mo.MapFrom(s => s.InstrumentId))
            .ForMember(d => d.Date, mo => mo.MapFrom(s => s.Date))
            .ForMember(d => d.Status, mo => mo.MapFrom(s => s.Status))
            .ForMember(d => d.OpeningPrice, mo => mo.MapFrom(s => s.OpeningPrice))
            .ForMember(d => d.HighestPrice, mo => mo.MapFrom(s => s.HighestPrice))
            .ForMember(d => d.LowestPrice, mo => mo.MapFrom(s => s.LowestPrice))
            .ForMember(d => d.LastPrice, mo => mo.MapFrom(s => s.LastPrice))
            .ForMember(d => d.ClosingPrice, mo => mo.MapFrom(s => s.ClosingPrice))
            .ForMember(d => d.PreviousClosingPrice, mo => mo.MapFrom(s => s.PreviousClosingPrice))
            .ForMember(d => d.UpperBoundPrice, mo => mo.MapFrom(s => s.UpperBoundPrice))
            .ForMember(d => d.LowerBoundPrice, mo => mo.MapFrom(s => s.LowerBoundPrice))
            .ForMember(d => d.NumberOfTrades, mo => mo.MapFrom(s => s.NumberOfTrades))
            .ForMember(d => d.TradingValue, mo => mo.MapFrom(s => s.TradingValue))
            .ForMember(d => d.TradingVolume, mo => mo.MapFrom(s => s.TradingVolume));
    }
}
