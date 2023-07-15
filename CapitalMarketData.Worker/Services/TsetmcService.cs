using CapitalMarketData.Worker.Models;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CapitalMarketData.Worker.Services;

public static class TsetmcService
{
    /// <summary>
    /// Gets INS codes of instruments from tsetmc.com.
    /// </summary>
    /// <returns>List of INS codes.</returns>
    public static async Task<List<string>> GetInsCodes()
    {
        List<string> insCodes = new();

        var url = $@"http://old.tsetmc.com/Loader.aspx?ParTree=151114";
        HttpClientHandler handler = new()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };
        HttpClient client = new(handler);
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        Regex pattern = new(@"ParTree=151311&i=\d+");
        if (pattern.IsMatch(responseString))
        {
            insCodes = pattern.Matches(responseString).Select(g => g.Value.Replace("ParTree=151311&i=", string.Empty)).Distinct().ToList();
        }

        return insCodes;
    }

    /// <summary>
    /// Gets basic information of an instrument from tsetmc.com.
    /// </summary>
    /// <param name="insCode">15 to 17 long number that tsetmc.com assigns to each instrument, for example 25631699615003698</param>
    /// <returns>Basic information of the instrument as InstrumentIdentityRoot type</returns>
    public static async Task<InstrumentIdentityRoot?> GetIntrumentInfo(string insCode)
    {
        ArgumentNullException.ThrowIfNull(insCode);

        var url = $@"http://cdn.tsetmc.com/api/Instrument/GetInstrumentIdentity/{insCode}";
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        InstrumentIdentityRoot? instrumentInfo = JsonSerializer.Deserialize<InstrumentIdentityRoot>(responseString);
        return instrumentInfo;
    }

    /// <summary>
    /// Gets trading data for institutional and individual clients of an instrument from tsetmc.com.
    /// </summary>
    /// <param name="insCode">15 to 17 long numbers that tsetmc.com assigns to each instrument, for example 25631699615003698</param>
    /// <returns>Institutional and individual trading data as ClientTypeData type</returns>
    public static async Task<ClientTypeData?> GetInstitutionalIndividualData(string insCode)
    {
        ArgumentNullException.ThrowIfNull(insCode);

        var url = $@"http://cdn.tsetmc.com/api/ClientType/GetClientType/{insCode}/1/0";
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        ClientTypeData? indiInstiData = JsonSerializer.Deserialize<ClientTypeData>(responseString);
        return indiInstiData;
    }

    /// <summary>
    /// Gets today price data (like closing, opening, high, low , ...) of an instrument from tsetmc.com.
    /// </summary>
    /// <param name="insCode">15 to 17 long numbers that tsetmc.com assigns to each instrument, for example 25631699615003698</param>
    /// <returns>Today price data as ClosingPriceDailyRoot type</returns>
    public static async Task<ClosingPriceDailyRoot?> GetPriceData(string insCode)
    {
        ArgumentNullException.ThrowIfNull(insCode);

        var url = $@"http://cdn.tsetmc.com/api/ClosingPrice/GetClosingPriceDaily/{insCode}/{DateTime.Now.Date:yyyyMMdd}";
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        ClosingPriceDailyRoot? priceData = JsonSerializer.Deserialize<ClosingPriceDailyRoot>(responseString);
        return priceData;
    }

    /// <summary>
    /// Gets today status of an instrument from tsetmc.com.
    /// </summary>
    /// <param name="insCode">15 to 17 long numbers that tsetmc.com assigns to each instrument, for example 25631699615003698</param>
    /// <returns>Today state of the instrument as the first element of InstrumentStateList type</returns>
    public static async Task<InstrumentState?> GetInstrumentState(string insCode)
    {
        ArgumentNullException.ThrowIfNull(insCode);

        var url = $@"http://cdn.tsetmc.com/api/MarketData/GetInstrumentState/{insCode}/{DateTime.Now.Date:yyyyMMdd}";
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        InstrumentStateList? stateList = JsonSerializer.Deserialize<InstrumentStateList>(responseString);
        return stateList?.instrumentState[0];
    }

    /// <summary>
    /// Gets toda allowed price range (static thresholds) for an instrument from tsetmc.com.
    /// </summary>
    /// <param name="insCode">15 to 17 long numbers that tsetmc.com assigns to each instrument, for example 25631699615003698</param>
    /// <returns>Today static threshold of the instrument as the second element of StaticThresholdList type</returns>
    public static async Task<StaticThreshold?> GetStaticThresholds(string insCode)
    {
        ArgumentNullException.ThrowIfNull(insCode);

        var url = $@"http://cdn.tsetmc.com/api/MarketData/GetStaticThreshold/{insCode}/{DateTime.Now.Date:yyyyMMdd}";
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        StaticThresholdList? staticThresholdList = JsonSerializer.Deserialize<StaticThresholdList>(responseString);
        return staticThresholdList?.staticThreshold[1];
    }
}
