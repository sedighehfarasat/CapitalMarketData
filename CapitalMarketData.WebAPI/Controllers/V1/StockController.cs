using AutoMapper;
using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CapitalMarketData.WebApi.Controllers.V1;

[ApiController]
[Route("api/v1/[Controller]")]
public class StockController : ControllerBase
{
//    private readonly IStockRepository _stockRepo;
//    private readonly IMapper _mapper;

//    public StockController(IStockRepository stockRepo, IMapper mapper)
//    {
//        _stockRepo = stockRepo;
//        _mapper = mapper;
//    }

//    // GET: api/v1/stock/StockById/[id]
//    [HttpGet("StockById/{id}")]
//    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StockViewModel))]
//    public async Task<IActionResult> GetStockById(string id)
//    {
//        if (string.IsNullOrEmpty(id)) return BadRequest();

//        var stock = await _stockRepo.GetById(id);
//        if (stock is null) return NotFound();

//        return Ok(_mapper.Map<StockViewModel>(stock));
//    }

//    // GET: api/v1/stock/StockByTicker/[ticker]
//    [HttpGet("StockByTicker/{ticker}")]
//    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StockViewModel))]
//    public async Task<IActionResult> GetStockByTicker(string ticker)
//    {
//        if (string.IsNullOrEmpty(ticker)) return BadRequest();

//        var stock = await _stockRepo.GetByTicker(ticker + "1");
//        if (stock is null) return NotFound();

//        return Ok(_mapper.Map<StockViewModel>(stock));
//    }
}