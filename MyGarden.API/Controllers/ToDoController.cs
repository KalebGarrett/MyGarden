using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyGarden.Models;

namespace MyGarden.API.Controllers;

[ApiController]
[Route("todos")]
public class ToDoController : ControllerBase
{
    private readonly GardenDbContext _context;

    public ToDoController(GardenDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("")]
    public async Task<ActionResult<ToDo>> Get()
    {
        var todo = await _context.ToDo.ToListAsync();

        if (todo.Count == 0)
        {
            return NotFound();
        }
        
        return Ok(todo);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDo>> Get(int id)
    {
        var todo = await _context.ToDo.FindAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost("")]
    public async Task<ActionResult<ToDo>> Create(ToDo toDo)
    {
        _context.ToDo.Add(toDo);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(Get), new {id = toDo.Id}, toDo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ToDo toDo)
    {
        if (id != toDo.Id)
        {
            return BadRequest();
        }

        _context.Entry(toDo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ToDoExists(id))
            {
                return NotFound(); 
            }
        }

        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var todo = await _context.ToDo.FindAsync(id);

        if (todo == null)
        {
            return NotFound(); 
        }

        _context.ToDo.Remove(todo);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ToDoExists(int id)
    {
        return _context.ToDo.Any(p => p.Id == id);
    }
}