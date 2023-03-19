using System.ComponentModel.DataAnnotations;

namespace treni_contact.Http.Requests.Contact;

public class ContactCreateRequest
{
    [Required, StringLength(100)]
    public string FirstName { get; set; }
    
    [Required, StringLength(100)]
    public string SecondName { get; set; }
    
    public DateTime? BirthDay { get; set; }
}