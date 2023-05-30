namespace CapitalMarketData.Entities.Entities;

public class NAV
{
    public NAV()
    {
        Date = DateTime.Now;
    }

    public string? InstrumentId { get; set; }
    public DateTime Date { get; private set; }
    public decimal? NetAssetValue { get; set; }
    
    public ETF? ETF { get; set; }
}
