using MetroClimate.Data.Models;

namespace MetroClimate.Data.Dtos;

public class AddStationDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public User User { get; set; }
    
}