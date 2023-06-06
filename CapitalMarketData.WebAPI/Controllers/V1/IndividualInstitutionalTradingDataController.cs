using AutoMapper;
using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CapitalMarketData.WebApi.Controllers.V1;

[ApiController]
[Route("api/v1/[Controller]")]
public class IndividualInstitutionalTradingDataController : ControllerBase
{
    private readonly IIndiInstiTradingDataRepository _indiInstiRepo;
    private readonly IMapper _mapper;

    public IndividualInstitutionalTradingDataController(IIndiInstiTradingDataRepository indiInstiRepo, IMapper mapper)
    {
        _indiInstiRepo = indiInstiRepo;
        _mapper = mapper;
    }

    // GET: api/v1/IndividualInstitutionalTradingData/[id]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IndiInstiTradingDataViewModel))]
    public async Task<IActionResult> GetTodayTradingDataByInstrumentId(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var indiInstidata = await _indiInstiRepo.GetTodayDataByInstrumentId(id);
        if (indiInstidata is null) return NotFound();

        return Ok(_mapper.Map<IndiInstiTradingDataViewModel>(indiInstidata));
    }
}