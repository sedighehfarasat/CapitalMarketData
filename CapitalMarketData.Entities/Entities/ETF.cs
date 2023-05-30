namespace CapitalMarketData.Entities.Entities;

public class ETF : Instrument
{
    public ETF()
    {
        NAV = new HashSet<NAV>();
    }

    public ICollection<NAV> NAV { get; set; }
}
