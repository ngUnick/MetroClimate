using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using MetroClimate.Api.Filters;
using MetroClimate.Data.Common;
using MetroClimate.Data.Constants;
using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Dtos.Payload;
using MetroClimate.Data.Models;
using Microsoft.AspNetCore.Mvc;
using MetroClimate.Services.Services;
using MetroClimate.Services.Validator;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace MetroClimate.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StationController : ControllerBase
{
    private readonly IStationService _stationService;
    private readonly IUserService _userService;
    private readonly MetroClimateDbContext _dbContext;

    public StationController(ILogger<WeatherForecastController> logger, 
        IStationService stationService, IUserService userService, MetroClimateDbContext dbContext)
    {
        _stationService = stationService;
        _userService = userService;
        _dbContext = dbContext;
    }

    [Authorization]
    [HttpGet(Name = "GetUserStations")] // "userId" is a placeholder for the actual user id
    public async Task<ApiResponse<IEnumerable<StationDto>?>> Get()
    {
        var user = HttpContext.Items["User"] as User;
        
        return new ApiResponse<IEnumerable<StationDto>?>(await _stationService.GetUserStationsAsync(user.Id));
        
        
    }
    
    [Authorization]
    [HttpPost(Name = "AddStation")] // "userId" is a placeholder for the actual user id
    public async Task<ApiResponse> Post([FromBody] AddStationPld addStationPld)
    {
        var user = HttpContext.Items["User"] as User;
        
        var validator = new AddStationValidator(user!, _dbContext);
        var validationResult = await validator.ValidateAsync(addStationPld);
        
        if (!validationResult.IsValid)
        {
            return new ApiResponse(ErrorCode.BadRequest, "Invalid data", validationResult);
        }
        
        
        var station = new Station
        {
            Id = addStationPld.Id,
            Name = addStationPld.Name,
            Description = addStationPld.Description,
            UserId = user!.Id
        };
        
        await _stationService.AddStationAsync(station);
        return new ApiResponse();
    }
    
    
}