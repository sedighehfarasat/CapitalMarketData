using AutoMapper;
using CapitalMarketData.Entities.DTOs;
using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.WebAPI.MappingProfiles;

public class IndiInstiTradingDataProfile : Profile
{
    public IndiInstiTradingDataProfile()
    {
        CreateMap<IndiInstiTradingData, IndiInstiTradingDataViewModel>()
            .ForMember(d => d.InstrumentId, mo => mo.MapFrom(s => s.InstrumentId))
            .ForMember(d => d.Date, mo => mo.MapFrom(s => s.Date))
            .ForMember(d => d.IndividualNumberOfTrades_BuySide, mo => mo.MapFrom(s => s.IndividualNumberOfTrades_BuySide))
            .ForMember(d => d.IndividualNumberOfTrades_SellSide, mo => mo.MapFrom(s => s.IndividualNumberOfTrades_SellSide))
            .ForMember(d => d.IndividualTradingVolume_BuySide, mo => mo.MapFrom(s => s.IndividualTradingVolume_BuySide))
            .ForMember(d => d.IndividualTradingVolume_SellSide, mo => mo.MapFrom(s => s.IndividualTradingVolume_SellSide))
            .ForMember(d => d.InstitutionalNumberOfTrades_BuySide, mo => mo.MapFrom(s => s.InstitutionalNumberOfTrades_BuySide))
            .ForMember(d => d.InstitutionalNumberOfTrades_SellSide, mo => mo.MapFrom(s => s.InstitutionalNumberOfTrades_SellSide))
            .ForMember(d => d.InstitutionalTradingVolume_BuySide, mo => mo.MapFrom(s => s.InstitutionalTradingVolume_BuySide))
            .ForMember(d => d.InstitutionalTradingVolume_SellSide, mo => mo.MapFrom(s => s.InstitutionalTradingVolume_SellSide));
    }
}
