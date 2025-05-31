using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;
using WebApplication7.Models.Dtos;

namespace WebApplication7.Controllers;

[ApiController]
[Route("/api/trips/{idTrip}/clients")]
public class ClientTripsController : ControllerBase
{
    private readonly TripDbContext _context;

    public ClientTripsController(TripDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AssignClientToTrip(
        int idTrip, 
        [FromBody] ClientTripDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (await _context.Clients.AnyAsync(c => c.Pesel == dto.Pesel))
            return BadRequest("Client with this PESEL already exists");

        var trip = await _context.Trips
            .Include(t => t.ClientTrips)
            .FirstOrDefaultAsync(t => t.IdTrip == idTrip);

        if (trip == null) return NotFound("Trip not found");
        if (trip.DateFrom <= DateTime.Now) 
            return BadRequest("Trip has already started");
        if (trip.ClientTrips.Any(ct => ct.Client.Pesel == dto.Pesel))
            return BadRequest("Client is already assigned to this trip");

        var client = new Client
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Pesel = dto.Pesel
        };

        var clientTrip = new ClientTrip
        {
            Client = client,
            Trip = trip,
            RegisteredAt = DateTime.Now,
            PaymentDate = dto.PaymentDate
        };

        await _context.ClientTrips.AddAsync(clientTrip);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(
            nameof(TripsController.GetTrip),
            "Trips",
            new { id = idTrip },
            new { clientTrip.IdClient, clientTrip.IdTrip });
    }
}