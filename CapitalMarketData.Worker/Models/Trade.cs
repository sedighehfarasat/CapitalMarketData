#nullable disable

namespace CapitalMarketData.Worker.Models;

public class Trade
{   
    public string ci { get; set; }
    public object inP { get; set; }
    public object cat { get; set; }
    public object a_tag { get; set; }
    public List<Header> header { get; set; }
    public MainData mainData { get; set; }
    public object futureData { get; set; }
    public object optionData { get; set; }
    public object ITables { get; set; }
    public object co { get; set; }
}

public class Header
{
    public string name { get; set; }
    public string namad { get; set; }
    public string state { get; set; }
    public string am { get; set; }
    public string dar { get; set; }
    public string time { get; set; }
}

public class MainData
{
    public object gtr_a { get; set; }
    public Ghp ghp { get; set; }
    public string dm { get; set; }
    public Bt bt { get; set; }
    public string hmo { get; set; }
    public string agh { get; set; }
    public string arm { get; set; }
    public string rgh { get; set; }
    public string tds { get; set; }
    public object eps { get; set; }
    public string ttm { get; set; }
    public string hma { get; set; }
    public string pe { get; set; }
    public string ttmpe { get; set; }
    public string ts { get; set; }
    public Bm bm { get; set; }
    public string arb { get; set; }
    public F2 f2 { get; set; }
    public string nav { get; set; }
    public object kh { get; set; }
    public object f { get; set; }
    public string kh_p { get; set; }
    public string f_p { get; set; }
    public object gtr_p { get; set; }
    public object bt_u { get; set; }
    public object bt_d { get; set; }
    public object hm { get; set; }
    public object ag { get; set; }
    public object am { get; set; }
    public object rg { get; set; }
    public object bm_u { get; set; }
    public object bm_d { get; set; }
    public object ang { get; set; }
    public object tm { get; set; }
    public object qeyemal { get; set; }
    public object r_b { get; set; }
    public object rb { get; set; }
}

public class Bm
{
    public string u { get; set; }
    public string d { get; set; }
}

public class Bt
{
    public string u { get; set; }
    public string d { get; set; }
}

public class F2
{
    public string u { get; set; }
    public string d { get; set; }
}

public class Ghp
{
    public string v { get; set; }
    public string p { get; set; }
}