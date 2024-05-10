using System;
using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Common;

namespace MetroClimate.Data.Models;

public class WeatherForecast : IRecordable
{
    public int Id { get; set; }
    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    
    [MaxLength(100)]
    public string? Summary { get; set; }
    
    public DateTime Created { get; set; }
    
    public DateTime Updated { get; set; }
}

