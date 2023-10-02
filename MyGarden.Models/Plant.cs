namespace MyGarden.Models;

public class Plant
{
    public string Name { get; set; }
    public DateTime PlantingDate { get; set; }
    public DateTime AdultDate { get; set; }
    public int WateringSchedule { get; set; }
    public string Description { get; set; }
}