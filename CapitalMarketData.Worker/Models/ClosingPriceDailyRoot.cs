#nullable disable

namespace CapitalMarketData.Worker.Models;

public class ClosingPriceDailyRoot
{
    public ClosingPriceDaily closingPriceDaily { get; set; }
}

public class ClosingPriceDaily
{
    public double priceChange { get; set; }
    public double priceMin { get; set; }
    public double priceMax { get; set; }
    public double priceYesterday { get; set; }
    public double priceFirst { get; set; }
    public bool last { get; set; }
    public int id { get; set; }
    public string insCode { get; set; }
    public int dEven { get; set; }
    public int hEven { get; set; }
    public double pClosing { get; set; }
    public bool iClose { get; set; }
    public bool yClose { get; set; }
    public double pDrCotVal { get; set; }
    public double zTotTran { get; set; }
    public double qTotTran5J { get; set; }
    public double qTotCap { get; set; }
}