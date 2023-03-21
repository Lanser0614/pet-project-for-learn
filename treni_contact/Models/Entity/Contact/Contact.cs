using Microsoft.AspNetCore.Identity;
using treni_contact.Models.Entity.Email;
using treni_contact.Models.Entity.Phone;
using treni_contact.Models.Entity.User;

namespace treni_contact.Models.Contact;

public class Contact
{
    public long? Id { get; set; }
    
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    
    public DateTime? BirthDay { get; set; }
    public ICollection<Phone>? Phones { get; set; }
    
    public ICollection<Email>? Emails { get; set; }
    
    // public Guid ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
}