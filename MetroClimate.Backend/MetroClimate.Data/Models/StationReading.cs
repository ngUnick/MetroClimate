using MetroClimate.Data.Common;
using MetroClimate.Data.Constants;

namespace MetroClimate.Data.Models;

public class StationReading : IRecordable
{
    public int Id { get; set; }
    public int StationId { get; set; }
    public int SensorId { get; set; }
    public double Value { get; set; }
    public Station Station{ get; set; } = null!;
    public Sensor Sensor { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}