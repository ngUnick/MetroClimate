using FluentValidation.Results;
using MetroClimate.Data.Common;
using MetroClimate.Data.Constants;
using MetroClimate.Data.Dtos;
using MetroClimate.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MetroClimate.Api.Filters;

public class AuthorizationAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userService = context.HttpContext.RequestServices.GetService<IUserService>();
        var headers = context.HttpContext.Request.Headers;

        var validationResult = new ValidationResult();

        if (!headers.ContainsKey("Authorization") || string.IsNullOrWhiteSpace(headers["Authorization"]))
        {
            validationResult.Errors.Add(new ValidationFailure("Authorization", "Authorization header is missing"));
            context.Result = new JsonResult(new ApiResponse<IEnumerable<FullStationReadingDto>?>(ErrorCode.Unauthorized, "Unauthorized", validationResult));
            return;
        }

        var user = await userService!.GetUserFromToken(headers.Authorization!);

        if (user == null)
        {
            validationResult.Errors.Add(new ValidationFailure("Authorization", "Invalid token"));
            context.Result = new ApiResponse<IEnumerable<FullStationReadingDto>?>(ErrorCode.Unauthorized, "Unauthorized", validationResult);
            return;
        }
        
        context.HttpContext.Items["User"] = user;

        await next();
    }
}