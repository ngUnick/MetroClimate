using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
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

    [HttpGet(Name = "GetUserStations")] // "userId" is a placeholder for the actual user id
    public async Task<ApiResponse<IEnumerable<StationDto>?>> Get()
    {
        var validationResult = new ValidationResult();
        
        var user = await _userService.GetUserFromToken(Request.Headers.Authorization!);
        
        if (user == null)
        {
            validationResult.Errors.Add(new ValidationFailure("Authorization", "Invalid token"));
            return new ApiResponse<IEnumerable<StationDto>?>(ErrorCode.Unauthorized, "Invalid data", validationResult);
        }
        
        if (Request.Headers.Authorization.Count == 0)
        {
            validationResult.Errors.Add(new ValidationFailure("Authorization", "Authorization header is missing"));
            return new ApiResponse<IEnumerable<StationDto>?>(ErrorCode.Unauthorized, "Invalid data", validationResult);
        }
        
        
        return new ApiResponse<IEnumerable<StationDto>?>(await _stationService.GetUserStationsAsync(user.Id));
        
        
    }
    
    [HttpPost(Name = "AddStation")] // "userId" is a placeholder for the actual user id
    public async Task<ApiResponse> Post([FromBody] AddStationPld addStationPld)
    {
        var validationResult = new ValidationResult();
        var user = await _userService.GetUserFromToken(Request.Headers.Authorization!);
        
        if (user == null)
        {
            validationResult.Errors.Add(new ValidationFailure("Authorization", "Invalid token"));
            return new ApiResponse(ErrorCode.Unauthorized, "Invalid data", validationResult);
        }
        
        var validator = new AddStationValidator(user, _dbContext);
        validationResult = await validator.ValidateAsync(addStationPld);
        
        if (Request.Headers.Authorization.Count == 0)
        {
            validationResult.Errors.Add(new ValidationFailure("Authorization", "Authorization header is missing"));
            return new ApiResponse(ErrorCode.Unauthorized, "Invalid data", validationResult);
        }
        
        if (!validationResult.IsValid)
        {
            return new ApiResponse(ErrorCode.BadRequest, "Invalid data", validationResult);
        }
        
        
        var station = new Station
        {
            Id = addStationPld.Id + "-" + user.Id,
            Name = addStationPld.Name,
            Description = addStationPld.Description,
            UserId = user.Id
        };
        
        await _stationService.AddStationAsync(station);
        return new ApiResponse();
    }
    
    
}