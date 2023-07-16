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

    // GET: api/v1/Instrument/ById/[id]
    [HttpGet("ById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InstrumentViewModel))]
    public async Task<IActionResult> GetById(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var instrument = await _instrumentRepo.GetById(id);
        if (instrument is null) return NotFound();

        return Ok(_mapper.Map<InstrumentViewModel>(instrument));
    }
}