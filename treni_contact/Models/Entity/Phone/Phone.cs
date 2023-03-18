namespace treni_contact.Models.Entity.Phone;

public class Phone
{
    public long id { get; set; }
    public long phone { get; set; }
    public DateTime created_at { get; set; }
    public Contact.Contact Contact { get; set; }
}