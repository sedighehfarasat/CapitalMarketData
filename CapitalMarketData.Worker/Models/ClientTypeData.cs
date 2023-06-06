#nullable disable

namespace CapitalMarketData.Worker.Models;

public class ClientTypeData
{   
    public ClientType clientType { get; set; }
}

public class ClientType
{
    public double buy_I_Volume { get; set; }
    public double buy_N_Volume { get; set; }
    public double buy_DDD_Volume { get; set; }
    public int buy_CountI { get; set; }
    public int buy_CountN { get; set; }
    public int buy_CountDDD { get; set; }
    public double sell_I_Volume { get; set; }
    public double sell_N_Volume { get; set; }
    public int sell_CountI { get; set; }
    public int sell_CountN { get; set; }
}
