using FluentValidation;
using MetroClimate.Data.Dtos.Payload;

namespace MetroClimate.Services.Validator;

public class LoginValidator : AbstractValidator<LoginPld>
{
    public LoginValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
    
}