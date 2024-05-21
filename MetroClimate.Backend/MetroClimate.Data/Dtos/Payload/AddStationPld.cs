namespace MetroClimate.Data.Dtos.Payload;

public class AddStationPld
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}