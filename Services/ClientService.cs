using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;
using WebApplication7.Models.Dtos;

namespace WebApplication7.Services;

public interface IClientService
{
    Task<bool> DeleteClientAsync(int idClient);
    Task<bool> AssignClientToTripAsync(int idTrip, ClientTripDto clientTripDto);
}




    public class ClientService : IClientService
    {
        private readonly TripDbContext _context;

        public ClientService(TripDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteClientAsync(int idClient)
        {
            var client = await _context.Clients
                .Include(c => c.ClientTrips)
                .FirstOrDefaultAsync(c => c.IdClient == idClient);

            if (client == null) return false;
            if (client.ClientTrips.Any()) return false; // Ma przypisane wycieczki

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignClientToTripAsync(int idTrip, ClientTripDto clientTripDto)
        {
            // Sprawdź czy klient z takim PESEL już istnieje
            if (await _context.Clients.AnyAsync(c => c.Pesel == clientTripDto.Pesel))
                return false;

            // Sprawdź czy wycieczka istnieje i czy jest w przyszłości
            var trip = await _context.Trips.FindAsync(idTrip);
            if (trip == null || trip.DateFrom <= DateTime.Now)
                return false;

            var client = new Client
            {
                FirstName = clientTripDto.FirstName,
                LastName = clientTripDto.LastName,
                Email = clientTripDto.Email,
                Telephone = clientTripDto.Telephone,
                Pesel = clientTripDto.Pesel
            };

            var clientTrip = new ClientTrip
            {
                Client = client,
                Trip = trip,
                RegisteredAt = DateTime.Now,
                PaymentDate = clientTripDto.PaymentDate
            };

            await _context.ClientTrips.AddAsync(clientTrip);
            await _context.SaveChangesAsync();
            return true;
        }
    }

