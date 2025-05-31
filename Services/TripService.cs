using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;
using WebApplication7.Models.Dtos;

// Services/ITripService.cs
public interface ITripService
{
    Task<PagedResult<TripDto>> GetTripsAsync(int page = 1, int pageSize = 10);
}

// Services/TripService.cs
public class TripService : ITripService
{
    private readonly TripDbContext _context;
    public TripService(TripDbContext context) => _context = context;

    public async Task<PagedResult<TripDto>> GetTripsAsync(int page = 1, int pageSize = 10)
    {
        var query = _context.Trips
            .Include(t => t.CountryTrips).ThenInclude(ct => ct.Country)
            .Include(t => t.ClientTrips).ThenInclude(ct => ct.Client)
            .OrderByDescending(t => t.DateFrom);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDto
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.CountryTrips.Select(ct => new CountryDto { Name = ct.Country.Name }).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientDto
                {
                    FirstName = ct.Client.FirstName,
                    LastName = ct.Client.LastName
                }).ToList()
            })
            .ToListAsync();

        return new PagedResult<TripDto>
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            Trips = items
        };
    }
}
