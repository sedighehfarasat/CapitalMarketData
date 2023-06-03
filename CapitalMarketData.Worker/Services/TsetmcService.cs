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
        var url = $@"http://old.tsetmc.com/tsev2/data/MarketWatchInit.aspx?h=0&r=0";
        HttpClientHandler handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        HttpClient client = new(handler);
        HttpResponseMessage response = await client.GetAsync(url);
        // TODO: Retry sending request in case of failing.
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();

        var rows = responseString.Replace(';', ',').Replace('-', ',').Replace('@', ',').Split(',');
        Regex pattern = new(@"^\d+$");
        foreach (var row in rows)
        {
            if (row.Length >= 15 && pattern.IsMatch(row))
            {
                insCodes.Add(row);
            }
        }

        return insCodes.Distinct().ToList();
    }

    /// <summary>
    /// Gets basic information of an instrument from tsetmc.com.
    /// </summary>
    /// <param name="insCode">15 to 17 long numbers that tsetmc.com assigns tto each instrument, for example 25631699615003698</param>
    /// <returns>Basic information of the instrument as InstrumentIdentityRoot type</returns>
    public static async Task<InstrumentIdentityRoot?> GetIntrumentInfo(string insCode)
    {
        ArgumentNullException.ThrowIfNull(insCode);

        var url = $@"http://cdn.tsetmc.com/api/Instrument/GetInstrumentIdentity/{insCode}";
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(url);
        // TODO: Retry sending request in case of failing.
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        InstrumentIdentityRoot? instrumentInfo = JsonSerializer.Deserialize<InstrumentIdentityRoot>(responseString);
        return instrumentInfo;
    }

    /// <summary>
    /// Gets trading data for institutional and individual clients of an instrument from tsetmc.com.
    /// </summary>
    /// <param name="insCode">15 to 17 long numbers that tsetmc.com assigns tto each instrument, for example 25631699615003698</param>
    /// <returns>Institutional and individual trading data as ClientTypeData type</returns>
    public static async Task<ClientTypeData?> GetInstitutionalIndividualData(string insCode)
    {
        ArgumentNullException.ThrowIfNull(insCode);

        var url = $@"http://cdn.tsetmc.com/api/ClientType/GetClientType/{insCode}/1/0";
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(url);
        // TODO: Retry sending request in case of failing.
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        ClientTypeData? indiInstiData = JsonSerializer.Deserialize<ClientTypeData>(responseString);
        return indiInstiData;
    }
}
