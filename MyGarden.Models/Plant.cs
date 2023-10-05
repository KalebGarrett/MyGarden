namespace MyGarden.Models;

public class Plant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime PlantingDate { get; set; }
    public int GrowthDuration { get; set; }
    public int WateringSchedule { get; set; }
    public string Description { get; set; }
}