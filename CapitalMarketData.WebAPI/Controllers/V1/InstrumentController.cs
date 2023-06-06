using AutoMapper;
using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CapitalMarketData.WebApi.Controllers.V1;

[ApiController]
[Route("api/v1/[Controller]")]
public class InstrumentController : ControllerBase
{
    private readonly IInstrumentRepository _instrumentRepo;
    private readonly IMapper _mapper;

    public InstrumentController(IInstrumentRepository instrumentRepo, IMapper mapper)
    {
        _instrumentRepo = instrumentRepo;
        _mapper = mapper;
    }

    // GET: api/v1/Instrument/Etfs
    [HttpGet("Etfs")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EtfViewModel>))]
    public async Task<IActionResult> GetAllEtfs()
    {
        var instruments = await _instrumentRepo.GetAll();
        if (instruments is null) return NotFound();

        return Ok(_mapper.Map<List<EtfViewModel>>(instruments));
    }

    // GET: api/v1/Instrument/InstrumentById/[id]
    [HttpGet("InstrumentById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EtfViewModel))]
    public async Task<IActionResult> GetInstrumentById(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var instrument = await _instrumentRepo.GetById(id);
        if (instrument is null) return NotFound();

        return Ok(_mapper.Map<EtfViewModel>(instrument));
    }

    // GET: api/v1/Instrument/InstrumentByTicker/[ticker]
    [HttpGet("InstrumentByTicker/{ticker}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EtfViewModel))]
    public async Task<IActionResult> GetEtfByTicker(string ticker)
    {
        if (string.IsNullOrEmpty(ticker)) return BadRequest();

        // To fix ی and ک in Persian.
        ticker = ticker.Replace((char)1740, (char)1610).Replace((char)1705, (char)1603);

        var instrument = await _instrumentRepo.GetByTicker(ticker);
        if (instrument is null) return NotFound();

        return Ok(_mapper.Map<EtfViewModel>(instrument));
    }
}