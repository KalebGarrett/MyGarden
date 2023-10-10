using Microsoft.EntityFrameworkCore;

namespace MyGarden.Models;

public class GardenDbContext : DbContext
{
    public GardenDbContext(DbContextOptions<GardenDbContext> options) : base(options)
    {
    }

    public DbSet<Plant> Plants { get; set; }
    public DbSet<ToDo> ToDo { get; set; }
}