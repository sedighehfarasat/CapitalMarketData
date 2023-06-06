using CapitalMarketData.Entities.Enums;

namespace CapitalMarketData.Worker.Helper;

public static class Convertor
{
    public static Status? ToStatusEnum(string str)
    {
        return str switch
        {
            "A " => Status.Trading,
            "IS" => Status.Halted,
            "I " => Status.Suspended,
            "AR" => Status.EnteringOrder,
            "AS" => Status.RemovingOrder,
            _ => null,
        };
    }
}
