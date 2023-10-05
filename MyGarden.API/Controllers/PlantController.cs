using System.Data;
using System.Data.SqlClient;
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
    public async Task<IActionResult> Get()
    {
        var plants = await _context.Plants.ToListAsync();
        return Ok(plants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Plant>> Get(int id)
    {
        return Ok(new Plant());
    }
    
    
    [HttpPost("")]
    public async Task<ActionResult<Plant>> Create(Plant plant)
    {
        return Created("", plant);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Plant>> Update(string id, Plant plant)
    {
        return Ok(plant);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Plant>> Delete(string id)
    {
        return NoContent();
    }
}