using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models;

[Table("Country")]
public class Country
{
    [Key]
    public int IdCountry { get; set; }

  
    [StringLength(120)]
    public string Name { get; set; }

    public virtual ICollection<CountryTrip> CountryTrips { get; set; }
}