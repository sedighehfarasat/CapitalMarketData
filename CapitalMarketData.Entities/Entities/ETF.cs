namespace CapitalMarketData.Entities.Entities;

public class ETF : Instrument
{
    private HashSet<NAV> _nav = new();
    public ICollection<NAV> NAV => _nav;
}
