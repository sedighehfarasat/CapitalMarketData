#nullable disable

namespace CapitalMarketData.Worker.Models;

public class InstrumentIdentityRoot
{
    public InstrumentIdentity instrumentIdentity { get; set; }
}

public class InstrumentIdentity
{
    public Sector sector { get; set; }
    public SubSector subSector { get; set; }
    public string cValMne { get; set; }
    public string lVal18 { get; set; }
    public string cSocCSAC { get; set; }
    public string lSoc30 { get; set; }
    public string yMarNSC { get; set; }
    public string yVal { get; set; }
    public string insCode { get; set; }
    public string lVal30 { get; set; }
    public string lVal18AFC { get; set; }
    public int flow { get; set; }
    public string cIsin { get; set; }
    public double zTitad { get; set; }
    public int baseVol { get; set; }
    public string instrumentID { get; set; }
    public string cgrValCot { get; set; }
    public string cComVal { get; set; }
    public int lastDate { get; set; }
    public int sourceID { get; set; }
    public string flowTitle { get; set; }
    public string cgrValCotTitle { get; set; }
}

public class Sector
{
    public int dEven { get; set; }
    public string cSecVal { get; set; }
    public string lSecVal { get; set; }
}

public class SubSector
{
    public int dEven { get; set; }
    public object cSecVal { get; set; }
    public int cSoSecVal { get; set; }
    public string lSoSecVal { get; set; }
}
