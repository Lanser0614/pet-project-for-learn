using System.ComponentModel.DataAnnotations;

namespace treni_contact.Http.Requests.Contact;

public class MassUpdateEmailForContactRequest
{
    public long ContactId { get; set; }

    public List<EmailUpdate> Emails { get; set; }
}

public class EmailUpdate
{
    [Required]
    public long id { get; set; }
    
    [Required, EmailAddress]
    public string email { get; set; }
}