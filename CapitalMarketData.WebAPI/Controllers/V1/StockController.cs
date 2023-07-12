using AutoMapper;
using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CapitalMarketData.WebApi.Controllers.V1;

[ApiController]
[Route("api/v1/[Controller]")]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepo;
    private readonly IMapper _mapper;

    public StockController(IStockRepository stockRepo, IMapper mapper)
    {
        _stockRepo = stockRepo;
        _mapper = mapper;
    }

    // GET: api/v1/Stock/Stocks
    [HttpGet("Stocks")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StockViewModel>))]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stockRepo.GetAll();
        if (stocks is null) return NotFound();

        return Ok(_mapper.Map<List<StockViewModel>>(stocks));
    }

    // GET: api/v1/Stock/ById/[id]
    [HttpGet("ById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StockViewModel))]
    public async Task<IActionResult> GetById(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var stock = await _stockRepo.GetById(id);
        if (stock is null) return NotFound();

        return Ok(_mapper.Map<StockViewModel>(stock));
    }

    // GET: api/v1/Stock/ByTicker/[ticker]
    [HttpGet("ByTicker/{ticker}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StockViewModel))]
    public async Task<IActionResult> GetByTicker(string ticker)
    {
        if (string.IsNullOrEmpty(ticker)) return BadRequest();

        // To fix ی and ک in Persian.
        ticker = ticker.Replace((char)1740, (char)1610).Replace((char)1705, (char)1603);

        var stock = await _stockRepo.GetByTicker(ticker);
        if (stock is null) return NotFound();

        return Ok(_mapper.Map<StockViewModel>(stock));
    }
}