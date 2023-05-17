using CapitalMarketData.Data;
using Microsoft.EntityFrameworkCore;

namespace CapitalMarketData.Tests;

public class WebApiTests
{
    [Fact]
    public void DateEquality()
    {
        var dbOption = new DbContextOptionsBuilder<CapitalMarketDataDbContext>()
                .UseSqlServer("Server=localhost;Initial catalog=Securities;User Id=sa;Password=Pa$$word#;TrustServerCertificate=True;")
                .Options;
        using(CapitalMarketDataDbContext _db = new(dbOption))
        {
            var isin = "IRO1ABAD0001";
            var data = _db.TradingData.FirstOrDefault(x => x.Date.Date == DateTime.Now.Date && x.InstrumentId == isin);
            var dataDate = data?.Date.Date;
            Assert.Equal(DateTime.Now.Date, dataDate);
        }
    }
}
