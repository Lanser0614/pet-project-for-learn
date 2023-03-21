using System.ComponentModel.DataAnnotations;
using treni_contact.Models.Entity.User;

namespace treni_contact.Http.Requests.Contact;

public class ContactCreateRequest
{
    [Required, StringLength(100)]
    public string FirstName { get; set; }
    
    [Required, StringLength(100)]
    public string SecondName { get; set; }
    
    public DateTime? BirthDay { get; set; }

    public string? UserName { get; set; }
    
}