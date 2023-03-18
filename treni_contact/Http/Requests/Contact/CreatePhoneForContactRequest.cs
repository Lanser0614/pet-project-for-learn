namespace treni_contact.Http.Requests.Contact;

public class CreatePhoneForContactRequest
{
    public long ContactId { get; set; }
    
    public List<Phone> Phones { get; set; }
}


public class Phone
{
    public long phone { get; set; }
 }