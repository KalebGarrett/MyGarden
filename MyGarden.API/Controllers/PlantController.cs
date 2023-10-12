using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyGarden.Models;

namespace MyGarden.API.Controllers;

[ApiController]
[Route("plants")]
public class PlantController : ControllerBase
{
    private readonly GardenDbContext _context;

    public PlantController(GardenDbContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public async Task<ActionResult<Plant>> Get()
    {
        var plant = await _context.Plants.ToListAsync();

        if (plant.Count == 0)
        {
            return NotFound();
        }
        
        return Ok(plant);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Plant>> Get(int id)
    {
        var plant = await _context.Plants.FindAsync(id);

        if (plant == null)
        {
            return NotFound();
        }

        return Ok(plant);
    }

    [HttpPost("")]
    public async Task<ActionResult<Plant>> Create(Plant plant)
    {
        _context.Plants.Add(plant);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(Get), new {id = plant.Id}, plant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Plant plant)
    {
        if (id != plant.Id)
        {
            return BadRequest();
        }

        _context.Entry(plant).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PlantExists(id))
            {
                return NotFound(); 
            }
        }

        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var plant = await _context.Plants.FindAsync(id);

        if (plant == null)
        {
            return NotFound(); 
        }

        _context.Plants.Remove(plant);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PlantExists(int id)
    {
        return _context.Plants.Any(p => p.Id == id);
    }
}