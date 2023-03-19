using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using treni_contact.Models.Entity.Email;
using treni_contact.Models.Entity.Phone;

namespace treni_contact.Models.Contact;

public class Contact
{
    public long? Id { get; set; }
    
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    
    public DateTime? BirthDay { get; set; }
    public ICollection<Phone> Phones { get; set; }
    
    public ICollection<Email> Emails { get; set; }
}