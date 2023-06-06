#nullable disable

namespace CapitalMarketData.Worker.Models;


public class InstrumentStateList
{
    public List<InstrumentState> instrumentState { get; set; }
}

public class InstrumentState
{
    public int idn { get; set; }
    public int dEven { get; set; }
    public int hEven { get; set; }
    public string insCode { get; set; }
    public string cEtaval { get; set; }
    public int realHeven { get; set; }
    public int underSupervision { get; set; }
    public object cEtavalTitle { get; set; }
}