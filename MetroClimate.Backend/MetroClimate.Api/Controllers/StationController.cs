using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Common;
using MetroClimate.Data.Constants;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Models;
using Microsoft.AspNetCore.Mvc;
using MetroClimate.Services.Services;
using MetroClimate.Services.Validator;

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
    public async Task<IEnumerable<StationDto>?> Get(int userId)
    {
        return await _stationService.GetUserStationsAsync(userId);
    }
    
    [HttpPost(Name = "SentStationReading")]
    public async Task<ApiResponse> SentStationReading(StationReadingPld reading)
    {
        var validator = new StationReadingValidator();
        var validationResult = await validator.ValidateAsync(reading);
        if (!validationResult.IsValid)
        {
            return new ApiResponse(ErrorCode.BadRequest, "Invalid data", validationResult);
        }

        await _stationService.RecordReadingAsync(reading);
        return new ApiResponse();
    }
    
}