using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Common;
using MetroClimate.Data.Constants;
using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Dtos.Payload;
using MetroClimate.Data.Models;
using Microsoft.AspNetCore.Mvc;
using MetroClimate.Services.Services;
using MetroClimate.Services.Validator;

namespace MetroClimate.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReadingController : ControllerBase
{
    private readonly IReadingService _readingService;
    private readonly MetroClimateDbContext _dbContext;

    public ReadingController(ILogger<WeatherForecastController> logger, IReadingService readingService, MetroClimateDbContext dbContext)
    {
        _dbContext = dbContext;
        _readingService = readingService;
    }


    [HttpGet(Name = "GetSensorReadings")] // "sensorId" is a placeholder for the actual sensor id
    public async Task<ApiResponse<IEnumerable<FullStationReadingDto>?>> Get(int userId, int sensorId)
    {
        return new ApiResponse<IEnumerable<FullStationReadingDto>?>(await _readingService.GetReadingsAsync(userId, sensorId));
    }
    
    [HttpPost(Name = "SentStationReading")]
    public async Task<ApiResponse> SentStationReading(StationReadingPld reading)
    {
        var validator = new StationReadingValidator(_dbContext);
        var validationResult = await validator.ValidateAsync(reading);
        if (!validationResult.IsValid)
        {
            return new ApiResponse(ErrorCode.BadRequest, "Invalid data", validationResult);
        }

        await _readingService.RecordReadingAsync(reading);
        return new ApiResponse();
    }
    
}