using MetroClimate.Data.Dtos;
using MetroClimate.Data.Models;
using Microsoft.AspNetCore.Mvc;
using MetroClimate.Services.Services;

namespace MetroClimate.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StationController : ControllerBase
{
    private readonly IStationService _stationService;

    public StationController(ILogger<WeatherForecastController> logger, IStationService stationService)
    {
        _stationService = stationService;
    }

    [HttpGet(Name = "GetUserStations")] // "userId" is a placeholder for the actual user id
    public async Task<IEnumerable<Station>?> Get(int userId)
    {
        return await _stationService.GetUserStationsAsync(userId);
    }
    
    [HttpPost(Name = "SentStationReading")]
    public async Task<IActionResult> SentStationReading(StationReadingPld reading)
    {
        

        await _stationService.RecordReadingAsync(reading);
        return Ok();
    }
    
}