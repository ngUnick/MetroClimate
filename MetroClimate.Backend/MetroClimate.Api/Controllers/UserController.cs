using FluentValidation.Results;
using MetroClimate.Data.Common;
using MetroClimate.Data.Constants;
using MetroClimate.Data.Dtos.Payload;
using MetroClimate.Services.Services;
using MetroClimate.Services.Validator;
using Microsoft.AspNetCore.Mvc;

namespace MetroClimate.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("login")]
    public async Task<ApiResponse<string?>> Login([FromBody] LoginPld loginPld)
    {
        var validator = new LoginValidator();
        var validationResult = await validator.ValidateAsync(loginPld);
        if (!validationResult.IsValid)
        {
            return new ApiResponse<string?>(ErrorCode.BadRequest, "Invalid data", validationResult);
        }
        
        var token = await _userService.Login(loginPld.Username, loginPld.Password);
        
        if (token != null) return new ApiResponse<string?>(token);
        
        validationResult.Errors.Add(new ValidationFailure("Username", "Invalid username or password"));
        return new ApiResponse<string?>(ErrorCode.BadRequest, "Invalid data", validationResult);
    }
    
}