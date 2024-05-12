using FluentValidation;
using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Dtos.Payload;

namespace MetroClimate.Services.Validator;

public class GetStationsValidator : AbstractValidator<GetStationsPld>
{
    public GetStationsValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0);
    }
}