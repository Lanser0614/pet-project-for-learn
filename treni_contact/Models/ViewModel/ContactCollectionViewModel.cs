namespace treni_contact.Models.ViewModel;

public class ContactCollectionViewModel
{
   public List<Models.Contact.Contact> Contacts { get; set; }
   public int Count { get; set; }
   
   public int PageSize { get; set; }
   public int PageNumber { get; set; }
}
