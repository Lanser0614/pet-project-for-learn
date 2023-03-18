namespace treni_contact.Http.Responses.Contact;

public class ContactUpdateResponse
{
   public long Id { get; set; }

   public ContactUpdateResponse(long id)
   {
      Id = id;
   }
}