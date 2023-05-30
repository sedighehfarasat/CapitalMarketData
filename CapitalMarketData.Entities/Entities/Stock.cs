using CapitalMarketData.Entities.Enums;

namespace CapitalMarketData.Entities.Entities;

public class Stock : Instrument
{
    public Board? Board { get; set; }
    public Industry? Industry { get; set; }
}
