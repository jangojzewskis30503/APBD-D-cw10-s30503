using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models.Dtos;

public class ClientTripDto
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    public string Telephone { get; set; }
    
    [Required]
    public string Pesel { get; set; }
    
    public DateTime? PaymentDate { get; set; }
}