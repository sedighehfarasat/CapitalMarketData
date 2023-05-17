using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CapitalMarketData.WebApi.Controllers.V1;

[ApiController]
[Route("api/v1/[Controller]")]
public class TradingDataController : ControllerBase
{
    private readonly ITradingDataRepository _tradingDataRepo;

    public TradingDataController(ITradingDataRepository tradingDataRepo)
    {
        _tradingDataRepo = tradingDataRepo;
    }

    // GET: api/v1/tradingdata/[id]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TradingData))]
    public async Task<IActionResult> GetTodayTradingDataByInstrumentId(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest();
        }
        else
        {
            var tradingdata = await _tradingDataRepo.GetTodayTradingDataByInstrumentId(id);
            if (tradingdata is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tradingdata);
            }
        }
    }
}