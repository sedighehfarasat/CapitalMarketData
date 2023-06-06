#nullable disable

namespace CapitalMarketData.Worker.Models;

public class Companies
{   
    public List<Category> companies { get; set; }
}

public class Category
{
    public string l { get; set; }
    public List<Stock> list { get; set; }
}

public class Stock
{
    public string n { get; set; }
    public string sy { get; set; }
    public string ic { get; set; }
    public string s { get; set; }
}