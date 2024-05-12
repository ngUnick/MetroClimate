using FluentValidation;
using MetroClimate.Data.Dtos;

namespace MetroClimate.Services.Validator;

public class StationReadingValidator : AbstractValidator<StationReadingPld>
{
    public StationReadingValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.StationId)
            .NotEmpty();
        
        RuleFor(x => x.SensorId)
            .NotEmpty();

        RuleFor(x => x.SensorType)
            .IsInEnum();
        
        RuleFor(x => x.Value)
            .NotNull()
            .NotEmpty();
    }
}