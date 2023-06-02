using AutoMapper;
using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CapitalMarketData.WebApi.Controllers.V1;

[ApiController]
[Route("api/v1/[Controller]")]
public class EtfController : ControllerBase
{
    private readonly IEtfRepository _etfRepo;
    private readonly IMapper _mapper;

    public EtfController(IEtfRepository etfRepo, IMapper mapper)
    {
        _etfRepo = etfRepo;
        _mapper = mapper;
    }

    // GET: api/v1/Etf/EtfById/[id]
    [HttpGet("EtfById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EtfViewModel))]
    public async Task<IActionResult> GetEtfkById(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var etf = await _etfRepo.GetById(id);
        if (etf is null) return NotFound();

        return Ok(_mapper.Map<EtfViewModel>(etf));
    }

    // GET: api/v1/Etf/EtfByTicker/[ticker]
    [HttpGet("EtfByTicker/{ticker}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EtfViewModel))]
    public async Task<IActionResult> GetEtfByTicker(string ticker)
    {
        if (string.IsNullOrEmpty(ticker)) return BadRequest();

        // To fix ی and ک in Persian.
        ticker = ticker.Replace((char)1740, (char)1610).Replace((char)1705, (char)1603);

        var etf = await _etfRepo.GetByTicker(ticker);
        if (etf is null) return NotFound();

        return Ok(_mapper.Map<EtfViewModel>(etf));
    }
}