using CapitalMarketData.Entities.Enums;

namespace CapitalMarketData.Entities.DTOs;

public class StockViewModel
{
    public string? Id { get; set; }
    public string? InsCode { get; set; }
    public string? Ticker { get; set; }
    public string? Name { get; set; }
    public Board? Board { get; set; }
    public Industry? Industry { get; set; }
}
