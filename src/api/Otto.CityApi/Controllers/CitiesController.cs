using AutoMapper;
using Otto.Storage.City;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otto.CityApi.Models;

namespace Otto.CityApi.Controllers;

[ApiController]
[Route("~/api/v1/cities/")]
public class CitiesController : Controller
{
    private readonly IMapper _mapper;
    private readonly CityDbContext _dbContext;
    private readonly ILogger<CitiesController> _logger;

    public CitiesController(ILogger<CitiesController> logger
        , CityDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    [HttpGet("name")]
    [ResponseCache(Duration = 1200, Location = ResponseCacheLocation.Any)]
    public async Task<ActionResult> ListCitiesAsync([FromQuery] string name, CancellationToken token)
    {
        var cities = await _dbContext.Cities
            .Where(city => EF.Functions.Like(city.Title, $"%{name}%"))
            .Take(10)
            .ToArrayAsync(cancellationToken: token);
        
        return Ok(_mapper.Map<IEnumerable<CityModel>>(cities));
    }
}