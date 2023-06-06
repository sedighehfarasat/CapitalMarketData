#nullable disable

namespace CapitalMarketData.Worker.Models;

public class StaticThresholdList
{
    public List<StaticThreshold> staticThreshold { get; set; }
}

public class StaticThreshold
{
    public string insCode { get; set; }
    public int dEven { get; set; }
    public int hEven { get; set; }
    public double psGelStaMax { get; set; }
    public double psGelStaMin { get; set; }
}