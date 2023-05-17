using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CapitalMarketData.WebApi.Controllers.V1;

[ApiController]
[Route("api/v1/[Controller]")]
public class InstrumentController : ControllerBase
{
    private readonly IInstrumentRepository _instrumentRepo;

    public InstrumentController(IInstrumentRepository instrumentRepo)
    {
        _instrumentRepo = instrumentRepo;
    }

    // GET: api/v1/instrument/[id]/InstrumentById
    [HttpGet("{id}/InstrumentById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Instrument))]
    public async Task<IActionResult> GetInstrumentById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest();
        }
        else
        {
            var instrument = await _instrumentRepo.GetInstrumentById(id);
            if (instrument is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(instrument);
            }
        }
    }

    // GET: api/v1/instrument/[ticker]/InstrumentByTicker
    [HttpGet("{ticker}/InstrumentByTicker")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Instrument))]
    public async Task<IActionResult> GetInstrumentByTicker(string ticker)
    {
        if (string.IsNullOrEmpty(ticker))
        {
            return BadRequest();
        }
        else
        {
            var instrument = await _instrumentRepo.GetInstrumentByTicker(ticker + "1");
            if (instrument is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(instrument);
            }
        }
    }
}