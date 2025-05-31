using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models;

[Table("Client_Trip")]
public class ClientTrip
{
    [Key, Column(Order = 0)]
    [ForeignKey("Client")]
    public int IdClient { get; set; }

    [Key, Column(Order = 1)]
    [ForeignKey("Trip")]
    public int IdTrip { get; set; }

  
    public DateTime RegisteredAt { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual Client Client { get; set; }
    public virtual Trip Trip { get; set; }
}