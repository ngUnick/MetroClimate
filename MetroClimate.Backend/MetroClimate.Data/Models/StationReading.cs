using MetroClimate.Data.Common;

namespace MetroClimate.Data.Models;

public class StationReading : IRecordable
{
    public int Id { get; set; }
    public int StationId { get; set; }
    public int SensorId { get; set; }
    public int Value { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}