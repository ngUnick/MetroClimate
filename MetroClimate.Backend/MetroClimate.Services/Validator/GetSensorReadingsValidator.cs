using FluentValidation;
using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos.Payload;
using Microsoft.EntityFrameworkCore;

namespace MetroClimate.Services.Validator;



public class GetSensorReadingsValidator : AbstractValidator<GetSensorReadingsPld>
{
    public GetSensorReadingsValidator(MetroClimateDbContext dbContext)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.SensorId)
            .NotNull()
            .NotEmpty()
            .MustAsync(async (sensorId, cancellationToken) =>
            {
                return await dbContext!.Sensors.AnyAsync(s => s.Id == sensorId, cancellationToken);
            })
            .WithMessage("Sensor not found");
    }
}