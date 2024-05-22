using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
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
    private readonly IUserService _userService;

    public StationController(ILogger<WeatherForecastController> logger, IStationService stationService, IUserService userService)
    {
        _stationService = stationService;
        _userService = userService;
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
    
    [HttpPost(Name = "AddStation")] // "userId" is a placeholder for the actual user id
    public async Task<ApiResponse> Post([FromBody] AddStationPld addStationPld)
    {
        var validator = new AddStationValidator();
        var validationResult = await validator.ValidateAsync(addStationPld);
        
        if (Request.Headers.Authorization.Count == 0)
        {
            validationResult.Errors.Add(new ValidationFailure("Authorization", "Authorization header is missing"));
            return new ApiResponse(ErrorCode.Unauthorized, "Invalid data", validationResult);
        }
        
        var user = await _userService.GetUserFromToken(Request.Headers.Authorization!);
        
        if (user == null)
        {
            validationResult.Errors.Add(new ValidationFailure("Authorization", "Invalid token"));
            return new ApiResponse(ErrorCode.Unauthorized, "Invalid data", validationResult);
        }
        
        var stationId = addStationPld.Id + "-" + user.Id;
        
        var stationExists = await _stationService.StationExists(stationId);
        
        if (stationExists)
        {
            validationResult.Errors.Add(new ValidationFailure("Id", "Station with this id already exists"));
            return new ApiResponse(ErrorCode.BadRequest, "Invalid data", validationResult);
        }
        
        if (!validationResult.IsValid)
        {
            return new ApiResponse(ErrorCode.BadRequest, "Invalid data", validationResult);
        }
        
        
        var station = new Station
        {
            Id = stationId,
            Name = addStationPld.Name,
            Description = addStationPld.Description,
            UserId = user.Id
        };
        
        await _stationService.AddStationAsync(station);
        return new ApiResponse();
    }
    
    
}