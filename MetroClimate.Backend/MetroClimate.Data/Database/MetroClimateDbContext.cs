using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MetroClimate.Data.Common;
using MetroClimate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MetroClimate.Data.Database;

public class MetroClimateDbContext : DbContext
{
    private readonly string? _schema;

    public MetroClimateDbContext(DbContextOptions<MetroClimateDbContext> options, IConfiguration configuration) : base(options)
    {
        _schema = configuration.GetConnectionString(name: "Schema");
    }
    
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (!string.IsNullOrWhiteSpace(_schema))
            modelBuilder.HasDefaultSchema(_schema);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var changedEntries = ChangeTracker.Entries()
            .Where(e => e is { Entity: IRecordable, State: EntityState.Added or EntityState.Modified }).ToList();

        var utcNow = DateTime.UtcNow;

        foreach (var entityEntry in changedEntries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((IRecordable)entityEntry.Entity).Created = utcNow;
                ((IRecordable)entityEntry.Entity).Updated = utcNow;

                continue;
            }

            ((IRecordable)entityEntry.Entity).Updated = utcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}