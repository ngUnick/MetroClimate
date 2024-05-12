using MetroClimate.Data.Models;
using MetroClimate.Data.Static;

namespace MetroClimate.Data.Dtos;

public class FullStationReadingDto : BaseStationReadingDto
{
    public string Unit { get; set; }
    public string Symbol { get; set; }
    
    public FullStationReadingDto(StationReading reading) : base(reading)
    {
        Unit = reading.Sensor.SensorType.Unit;
        Symbol = reading.Sensor.SensorType.Symbol;
        Value = reading.Sensor.SensorType.Formula is not null ? Formula.EvaluateFormula(reading.Value, reading.Sensor.SensorType.Formula) : reading.Value;
    }
    
    public FullStationReadingDto(StationReading reading, SensorType sensorType) : base(reading, sensorType)
    {
        Unit = sensorType.Unit;
        Symbol = sensorType.Symbol;
        Value = sensorType.Formula is not null ? Formula.EvaluateFormula(reading.Value, sensorType.Formula) : reading.Value;
    }
}