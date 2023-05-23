﻿namespace CapitalMarketData.Entities.Entities;

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

    public string? Ticker { get; set; }

    /// <summary>
    /// Gets or sets the full name of the instrument.
    /// </summary>
    public string? Name { get; set; }
}
