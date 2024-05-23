using FluentValidation;
using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Dtos.Payload;
using MetroClimate.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MetroClimate.Services.Validator;

public class AddStationValidator : AbstractValidator<AddStationPld>
{
    public AddStationValidator(User user, MetroClimateDbContext dbContext)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Length(10)
            .MustAsync(async (id, cancellationToken) =>
            {
                return !await dbContext!.Stations.AnyAsync(s => s.Id == id && s.UserId == user.Id);
            })
            .WithMessage("Station with this id already exists");
            
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