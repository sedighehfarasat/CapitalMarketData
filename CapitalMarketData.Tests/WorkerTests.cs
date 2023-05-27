using CapitalMarketData.Worker.Services;

namespace CapitalMarketData.Tests;

public class WorkerTests
{
    [Fact]
    public async Task TseService_ValidIsin()
    {
        string isin = "IRO1FKHZ0001";
        var data = await TseService.FetchLiveData(isin);
        Assert.NotNull(data);
    }

    [Fact]
    public async Task TseService_NullIsin()
    {
        string isin = null!;
        var caughtException = await Assert.ThrowsAsync<ArgumentNullException>(() => TseService.FetchLiveData(isin));
        Assert.Equal("Value cannot be null. (Parameter 'isin')", caughtException.Message);
    }

    [Fact]
    public async Task TsetmcService_GetInsCodesFromFile()
    {
        var data = await TsetmcService.GetInsCodesFromFile();
        Assert.NotNull(data);
    }

    [Fact]
    public async Task TsetmcService_GetIntrumentInfo()
    {
        string insCode = "25631699615003698";
        var data = await TsetmcService.GetIntrumentInfo(insCode);
        Assert.NotNull(data);
    }
}
