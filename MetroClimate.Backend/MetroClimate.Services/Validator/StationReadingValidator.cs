using FluentValidation;
using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Dtos.Payload;
using Microsoft.EntityFrameworkCore;

namespace MetroClimate.Services.Validator;

public class StationReadingValidator : AbstractValidator<StationReadingPld>
{
    public StationReadingValidator(MetroClimateDbContext dbContext)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.StationId)
            .NotNull()
            .NotEmpty()
            .MustAsync(async (stationId, cancellationToken) =>
            {
                return await dbContext!.Stations.AnyAsync(s => s.Id == stationId, cancellationToken);
            })
            .WithMessage("Station not found");
        
        RuleFor(x => x.SensorId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.SensorName)
            .NotEmpty()
            .Length(1, 15);

        RuleFor(x => x.SensorType)
            .IsInEnum();
        
        RuleFor(x => x.Value)
            .NotNull()
            .NotEmpty();
    }
}