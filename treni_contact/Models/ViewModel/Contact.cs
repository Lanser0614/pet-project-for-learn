namespace treni_contact.Models.ViewModel;

public class Contact
{
    public Contact(long id, string firstName, string secondName)
    {
        Id = id;
        FirstName = firstName;
        SecondName = secondName;
    }
    
    public long Id { get; set; }
    
    public string FirstName { get; set; }
    public string SecondName { get; set; }
}