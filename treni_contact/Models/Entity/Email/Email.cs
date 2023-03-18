namespace treni_contact.Models.Entity.Email;

public class Email
{
    public long id { get; set; }
    public string email { get; set; }
    public DateTime created_at { get; set; }
    public Contact.Contact Contact { get; set; }
}