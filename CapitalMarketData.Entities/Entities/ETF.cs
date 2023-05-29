namespace CapitalMarketData.Entities.Entities;

public class ETF : Instrument
{
    public ETF()
    {
        TradingData = new HashSet<TradingData>();
        NAV = new HashSet<NAV>();
    }

    public ICollection<TradingData> TradingData { get; set; }
    public ICollection<NAV> NAV { get; set; }
}
