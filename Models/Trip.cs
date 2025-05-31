using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models;

[Table("Trip")]
public class Trip
{
    [Key]
    public int IdTrip { get; set; }

  
    [StringLength(120)]
    public string Name { get; set; }


    [StringLength(220)]
    public string Description { get; set; }

 
    public DateTime DateFrom { get; set; }


    public DateTime DateTo { get; set; }


    public int MaxPeople { get; set; }

    public virtual ICollection<ClientTrip> ClientTrips { get; set; }
    public virtual ICollection<CountryTrip> CountryTrips { get; set; }
}