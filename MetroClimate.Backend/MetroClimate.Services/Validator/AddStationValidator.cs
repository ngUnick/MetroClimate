using FluentValidation;
using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos.Payload;

namespace MetroClimate.Services.Validator;

public class AddStationValidator : AbstractValidator<AddStationPld>
{
    public AddStationValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Length(10);
            
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
    }
    
}