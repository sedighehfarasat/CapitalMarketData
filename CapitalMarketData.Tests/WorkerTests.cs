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
        var caughtException = Assert.ThrowsAsync<ArgumentNullException>(() => TseService.FetchLiveData(isin));
        Assert.Equal("Value cannot be null. (Parameter 'isin')", caughtException.Result.Message);
    }
}
