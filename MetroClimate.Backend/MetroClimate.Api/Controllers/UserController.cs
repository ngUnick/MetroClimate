using FluentValidation.Results;
using MetroClimate.Data.Common;
using MetroClimate.Data.Constants;
using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Dtos.Payload;
using MetroClimate.Data.Dtos.Response;
using MetroClimate.Services.Services;
using MetroClimate.Services.Validator;
using Microsoft.AspNetCore.Mvc;

namespace MetroClimate.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly MetroClimateDbContext _dbContext;
    
    public UserController(IUserService userService, MetroClimateDbContext dbContext)
    {
        _userService = userService;
        _dbContext = dbContext;
    }
    
    [HttpPost("login")]
    public async Task<ApiResponse<LoginResponse>> Login([FromBody] LoginPld loginPld)
    {
        var validator = new LoginValidator();
        var validationResult = await validator.ValidateAsync(loginPld);
        
        if (!validationResult.IsValid)
        {
            return new ApiResponse<LoginResponse>(ErrorCode.BadRequest, "Invalid data", validationResult);
        }
        
        var (token, user) = await _userService.Login(loginPld.Username, loginPld.Password);
        
        if (token == null || user == null)
        {
            validationResult.Errors.Add(new ValidationFailure("Username", "Invalid username or password"));
            return new ApiResponse<LoginResponse>(ErrorCode.BadRequest, "Invalid data", validationResult);
        }
        
        var userDto = new UserDto(user!);
        
        return new ApiResponse<LoginResponse>(new LoginResponse
        {
            Token = token!,
            User = userDto
        });
        
        
        
        
    }
    
    [HttpPost("register")]
    public async Task<ApiResponse<LoginResponse>> Register([FromBody] RegisterPld registerPld)
    {
        var validator = new RegisterValidator(_dbContext);
        var validationResult = await validator.ValidateAsync(registerPld);
        if (!validationResult.IsValid)
        {
            return new ApiResponse<LoginResponse>(ErrorCode.BadRequest, "Invalid data", validationResult);
        }
        
        var (token, user) = await _userService.Register(registerPld);
        
        if (token == null || user == null)
        {
            validationResult.Errors.Add(new ValidationFailure("Username", "Username already exists"));
            return new ApiResponse<LoginResponse>(ErrorCode.BadRequest, "Invalid data", validationResult);
        }
        
        var userDto = new UserDto(user!);
        
        return new ApiResponse<LoginResponse>(new LoginResponse
        {
            Token = token!,
            User = userDto
        });
    }
    
}