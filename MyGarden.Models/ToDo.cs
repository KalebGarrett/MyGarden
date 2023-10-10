namespace MyGarden.Models;

public class ToDo
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; }
}