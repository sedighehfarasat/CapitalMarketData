using AutoMapper;
using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CapitalMarketData.WebApi.Controllers.V1;

[ApiController]
[Route("api/v1/[Controller]")]
public class TradingDataController : ControllerBase
{
    private readonly ITradingDataRepository _tradingDataRepo;
    private readonly IMapper _mapper;

    public TradingDataController(ITradingDataRepository tradingDataRepo, IMapper mapper)
    {
        _tradingDataRepo = tradingDataRepo;
        _mapper = mapper;
    }

    // GET: api/v1/tradingdata/[id]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TradingDataViewModel))]
    public async Task<IActionResult> GetTodayTradingDataByInstrumentId(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var tradingdata = await _tradingDataRepo.GetTodayDataByInstrumentId(id);
        if (tradingdata is null) return NotFound();

        return Ok(_mapper.Map<TradingDataViewModel>(tradingdata));
    }
}