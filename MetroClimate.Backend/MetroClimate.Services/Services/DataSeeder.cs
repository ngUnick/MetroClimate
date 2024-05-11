using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MetroClimate.Data.Database;
using MetroClimate.Data.Models;

namespace MetroClimate.Services.Services;
public class DataSeeder
{
    private readonly MetroClimateDbContext _dbContext;

    public DataSeeder(MetroClimateDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Seed<T>(JObject seedObject, string key) where T : class
    {
        var entities = ((JArray) seedObject[key]!).ToObject<IList<T>>()!;
        
        await _dbContext.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Seed()
    {
        var seedJson = await File.ReadAllTextAsync("../seed.json");
        var seedObject = JObject.Parse(seedJson);

        if(await _dbContext.SensorTypes.AnyAsync())
        {
            return;
        }
        
        await SeedMany<SensorType>(seedObject, "SensorType");
        await SeedMany<Station>(seedObject, "Station");
        // await SeedMany<Sensor>(seedObject, "Sensor");
        await _dbContext.SaveChangesAsync();
    }
    private async Task SeedMany<T>(JObject seedObject, string key) where T : class
    {
        var entities = ((JArray) seedObject[key]!).ToObject<IList<T>>()!;
        
        await _dbContext.AddRangeAsync(entities);
    }
}
