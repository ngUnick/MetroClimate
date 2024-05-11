using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MetroClimate.Data.Common;
using MetroClimate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MetroClimate.Data.Database;

public class MetroClimateDbContext : DbContext
{
    private readonly string? _schema;

    public MetroClimateDbContext(DbContextOptions<MetroClimateDbContext> options, IConfiguration configuration) : base(options)
    {
        _schema = configuration.GetConnectionString(name: "Schema");
    }
    
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<SensorType> SensorTypes { get; set; }
    public DbSet<StationReading> StationReadings { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (!string.IsNullOrWhiteSpace(_schema))
            modelBuilder.HasDefaultSchema(_schema);
        
        modelBuilder.Entity<StationReading>()
            .HasOne(sr => sr.Station)
            .WithMany(s => s.Readings)
            .HasForeignKey(sr => sr.StationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Sensor>()
            .HasOne(s => s.SensorType)
            .WithMany()
            .HasForeignKey(s => s.SensorTypeId);
            
            
            
            
            
            
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