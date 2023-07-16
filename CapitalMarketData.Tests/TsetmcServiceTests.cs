using CapitalMarketData.Worker.Services;

namespace CapitalMarketData.Tests;

public class TsetmcServiceTests
{

    [Fact]
    public async Task GetInsCodes()
    {
        var insCodes = await TsetmcService.GetInsCodes();
        Assert.NotNull(insCodes);
    }

    [Fact]
    public async Task GetIntrumentInfo()
    {
        string insCode = "24651394045981418";
        var data = await TsetmcService.GetIntrumentInfo(insCode);
        Assert.NotNull(data);
    }

    [Fact]
    public async Task GetInstitutionalIndividualData()
    {
        string insCode = "24651394045981418";
        await TsetmcService.GetInstitutionalIndividualData(insCode);
    }

    [Fact]
    public async Task GetPriceData()
    {
        string insCode = "24651394045981418";
        await TsetmcService.GetPriceData(insCode);
    }

    [Fact]
    public async Task GetInstrumentState()
    {
        string insCode = "24651394045981418";
        await TsetmcService.GetInstrumentState(insCode);
    }

    [Fact]
    public async Task GetStaticThresholds()
    {
        string insCode = "24651394045981418";
        await TsetmcService.GetStaticThresholds(insCode);
    }
}
