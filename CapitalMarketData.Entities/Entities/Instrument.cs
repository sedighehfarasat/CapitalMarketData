using CapitalMarketData.Entities.Enums;

namespace CapitalMarketData.Entities.Entities;

public abstract class Instrument
{
    /// <summary>
    /// Gets or sets the instrument's ISIN code.
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// Gets or sets INS code of the instrument. INS code is a unique code of an instrument in tsetmc.com.
    /// </summary>
    public string? InsCode { get; set; }
    /// <summary>
    /// Gets or sets the symbol of the instrument.
    /// </summary>
    public string? Ticker { get; set; }
    /// <summary>
    /// Gets or sets the full name of the instrument.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Gets or sets the type of the instrument (yval).
    /// </summary>
    public InstrumentType? Type { get; set; }
    /// <summary>
    /// Gets or sets the industry of the instrument.
    /// </summary>
    public Sector? Sector { get; set; }
    /// <summary>
    /// Gets or sets the subsector of the industry.
    /// </summary>
    public Subsector? Subsector { get; set; }

    private HashSet<TradingData> _tradingData = new();
    public ICollection<TradingData> TradingData => _tradingData;

    private HashSet<IndiInstiTradingData> _indiInstiTradingData = new();
    public ICollection<IndiInstiTradingData> IndiInstiTradingData => _indiInstiTradingData;
}
