using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Common;
using MetroClimate.Data.Constants;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Dtos.Payload;
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
    public async Task<ApiResponse<IEnumerable<StationDto>?>> Get([FromQuery] GetStationsPld payload)
    {
        var validator = new GetStationsValidator();
        var validationResult = await validator.ValidateAsync(payload);
        if (!validationResult.IsValid)
        {
            return new ApiResponse<IEnumerable<StationDto>?>(ErrorCode.BadRequest, "Invalid data", validationResult);
        }
        return new ApiResponse<IEnumerable<StationDto>?>(await _stationService.GetUserStationsAsync(payload.UserId));
    }
    
    
}