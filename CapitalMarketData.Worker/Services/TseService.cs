using System.Text.Json;
using CapitalMarketData.Worker.Models;

namespace CapitalMarketData.Worker.Services;

public static class TseService
{
    /// <summary>
    /// Gets today trading data for specified ISIN from tse.ir API.
    /// </summary>
    /// <param name="isin">12 digits code of an instrument e.g. IRO1FKHZ0001.</param>
    /// <returns>Trading data as Trade type.</returns>
    public static async Task<Trade?> FetchLiveData(string isin)
    {
        ArgumentNullException.ThrowIfNull(isin);

        var url = $@"https://tse.ir/json/Instrument/info_{isin}.json";
        HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync(url);
        // TODO: Retry sending request in case of failing.
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        Trade? instrument = JsonSerializer.Deserialize<Trade>(responseString);
        return instrument;
    }

    /// <summary>
    /// Gets list of stocks available in TSE from tse.ir API.
    /// </summary>
    /// <returns>List of stock information as Companies type</returns>
    public static async Task<Companies?> GetStockList()
    {
        var url = @"https://tse.ir/json/Listing/ListingByName1.json";
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(url);
        // TODO: Retry sending request in case of failing.
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        Companies? companies = JsonSerializer.Deserialize<Companies>(responseString);
        return companies;
    }
}
