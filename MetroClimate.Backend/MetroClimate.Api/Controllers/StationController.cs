using MetroClimate.Data.Models;
using Microsoft.AspNetCore.Mvc;
using MetroClimate.Services.Services;

namespace MetroClimate.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SationController : ControllerBase
{
    private readonly IStationService _stationService;

    public SationController(ILogger<WeatherForecastController> logger, IStationService stationService)
    {
        _stationService = stationService;
    }

    [HttpGet(Name = "GetUserStations")] // "userId" is a placeholder for the actual user id
    public async Task<IEnumerable<Station>?> Get(int userId)
    {
        return await _stationService.GetUserStationsAsync(userId);
    }
}