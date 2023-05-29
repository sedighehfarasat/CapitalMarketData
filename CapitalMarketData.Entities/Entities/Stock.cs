using CapitalMarketData.Entities.Enums;

namespace CapitalMarketData.Entities.Entities;

public class Stock : Instrument
{
    public Stock()
    {
        TradingData = new HashSet<TradingData>();
    }

    public Board? Board { get; set; }
    public Industry? Industry { get; set; }

    public ICollection<TradingData> TradingData { get; set; }
}
