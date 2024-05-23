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
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (!string.IsNullOrWhiteSpace(_schema))
            modelBuilder.HasDefaultSchema(_schema);

        modelBuilder.Entity<Sensor>()
            .HasOne(s => s.Station)
            .WithMany(s => s.Sensors)
            .HasForeignKey(s => s.StationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Sensor>()
            .HasOne(s => s.SensorType)
            .WithMany()
            .HasForeignKey(s => s.SensorTypeId);
        
        modelBuilder.Entity<Sensor>()
            .HasMany(s => s.Readings)
            .WithOne(sr => sr.Sensor)
            .HasForeignKey(sr => sr.SensorId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Station>()
            .HasMany(s => s.Sensors)
            .WithOne(s => s.Station)
            .HasForeignKey(s => s.StationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Station>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Station>()
            .HasOne(s => s.User)
            .WithMany(u => u.Stations)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
            
            
            
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