using CapitalMarketData.Worker.Services;

namespace CapitalMarketData.Tests;

public class TsetmcServiceTests
{

    [Fact]
    public async Task TsetmcService_GetInsCodesFromFile()
    {
        var data = await TsetmcService.GetInsCodesFromFile();
        Assert.NotNull(data);
    }

    [Fact]
    public async Task TsetmcService_GetIntrumentInfo()
    {
        string insCode = "24651394045981418";
        var data = await TsetmcService.GetIntrumentInfo(insCode);
        Assert.NotNull(data);
    }

    [Fact]
    public async Task TsetmcService_GetInsCodes()
    {
        await TsetmcService.GetInsCodes();
    }
}
