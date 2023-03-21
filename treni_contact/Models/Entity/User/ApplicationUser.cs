using Microsoft.AspNetCore.Identity;

namespace treni_contact.Models.Entity.User;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Contact.Contact> Contacts { get; set; }
}