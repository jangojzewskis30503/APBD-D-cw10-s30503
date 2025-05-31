using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly TripDbContext _context;

    public ClientsController(TripDbContext context)
    {
        _context = context;
    }

    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var client = await _context.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);

        if (client == null) return NotFound();
        if (client.ClientTrips.Any()) 
            return BadRequest("Client has assigned trips");

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}