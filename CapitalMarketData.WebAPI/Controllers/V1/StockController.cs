using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CapitalMarketData.WebApi.Controllers.V1;

[ApiController]
[Route("api/v1/[Controller]")]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepo;

    public StockController(IStockRepository stockRepo)
    {
        _stockRepo = stockRepo;
    }

    // GET: api/v1/stock/[id]/StockById
    [HttpGet("{id}/StockById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Stock))]
    public async Task<IActionResult> GetStockById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest();
        }
        else
        {
            var stock = await _stockRepo.GetById(id);
            if (stock is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(stock);
            }
        }
    }

    // GET: api/v1/stock/[ticker]/StockByTicker
    [HttpGet("{ticker}/StockByTicker")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Stock))]
    public async Task<IActionResult> GetStockByTicker(string ticker)
    {
        if (string.IsNullOrEmpty(ticker))
        {
            return BadRequest();
        }
        else
        {
            var stock = await _stockRepo.GetByTicker(ticker + "1");
            if (stock is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(stock);
            }
        }
    }
}