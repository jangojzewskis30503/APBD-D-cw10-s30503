using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models;

[Table("Client")]
public class Client
{
    [Key]
    public int IdClient { get; set; }

 
    [StringLength(120)]
    public string FirstName { get; set; }


    [StringLength(120)]
    public string LastName { get; set; }

 
    [StringLength(120)]
    public string Email { get; set; }


    [StringLength(120)]
    public string Telephone { get; set; }


    [StringLength(120)]
    public string Pesel { get; set; }

    public virtual ICollection<ClientTrip> ClientTrips { get; set; }
}