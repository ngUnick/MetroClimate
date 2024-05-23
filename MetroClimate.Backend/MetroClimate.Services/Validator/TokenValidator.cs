using FluentValidation.Results;
using MetroClimate.Data.Models;
using MetroClimate.Services.Services;
using Microsoft.AspNetCore.Http;

namespace MetroClimate.Services.Validator;

public static class TokenValidator
{
    public static async Task<(User?, ValidationResult)> ValidateToken(IUserService userService, IHeaderDictionary headers)
    {
        var validationResult = new ValidationResult();
        
        if (headers.Authorization.Count == 0)
        {
            validationResult.Errors.Add(new ValidationFailure("Authorization", "Authorization header is missing"));
            return (null, validationResult);
        }

        var user = await userService.GetUserFromToken(headers.Authorization!);
        
        if (user == null)
        {
            validationResult.Errors.Add(new ValidationFailure("Authorization", "Invalid token"));
            return (null, validationResult);
        }

        return (user, validationResult);
    }
}