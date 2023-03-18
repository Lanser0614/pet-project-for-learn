namespace treni_contact.Http.Responses.Contact;

public class ContactCreateResponse
{
   public long Id { get; set; }

   public ContactCreateResponse(long id)
   {
      Id = id;
   }
}