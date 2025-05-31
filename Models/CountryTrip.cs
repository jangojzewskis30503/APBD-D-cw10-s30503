using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models;

[Table("Country_Trip")]
public class CountryTrip
{
    [Key, Column(Order = 0)]
    [ForeignKey("Country")]
    public int IdCountry { get; set; }

    [Key, Column(Order = 1)]
    [ForeignKey("Trip")]
    public int IdTrip { get; set; }

    public virtual Country Country { get; set; }
    public virtual Trip Trip { get; set; }
}