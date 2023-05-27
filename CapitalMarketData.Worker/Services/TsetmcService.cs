using CapitalMarketData.Worker.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CapitalMarketData.Worker.Services;

public static class TsetmcService
{
    /// <summary>
    /// Gets INS codes of instruments from a file that is copied from http://www.tsetmc.com/tsev2/data/MarketWatchInit.aspx?h=0&r=0
    /// </summary>
    /// <returns>List of INS codes.</returns>
    public static async Task<List<string>> GetInsCodesFromFile()
    {
        List<string> insCodes = new();

        using (StreamReader reader = new("F:\\Algorithmic Trading\\Capital Market Data\\Documents\\InsText.txt"))
        {
            var text = await reader.ReadToEndAsync();
            var rows = text.Replace(';', ',').Replace('-', ',').Replace('@', ',').Split(',');

            Regex pattern = new(@"^\d+$");

            foreach (var row in rows)
            {
                if (row.Length >= 15 && pattern.IsMatch(row))
                {
                    insCodes.Add(row);
                }
            }
            return insCodes;
        }
    }

    /// <summary>
    /// Gets basic information of an instrument from tsetmc.com.
    /// </summary>
    /// <param name="insCode">15 to 17 long numbers that tsetmc.com assigns tto each instrument, for example 25631699615003698</param>
    /// <returns></returns>
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
}
