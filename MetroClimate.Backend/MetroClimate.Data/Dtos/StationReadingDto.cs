using MetroClimate.Data.Models;
using MetroClimate.Data.Static;

namespace MetroClimate.Data.Dtos;

public class StationReadingDto
{
    public int Id { get; set; }
    public int StationId { get; set; }
    public int SensorId { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; }
    public string Symbol { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    
    public StationReadingDto(StationReading reading, SensorType sensorType)
    {
        Id = reading.Id;
        StationId = reading.StationId;
        SensorId = reading.SensorId;
        Value = sensorType.Formula is not null ? Formula.EvaluateFormula(reading.Value, sensorType.Formula) : reading.Value;
        Unit = sensorType.Unit;
        Symbol = sensorType.Symbol;
        Created = reading.Created;
        Updated = reading.Updated;
    }
}