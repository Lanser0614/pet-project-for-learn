using System.ComponentModel.DataAnnotations;

namespace treni_contact.Http.Requests.Contact;

public class MassCreateEmailForContactRequest
{
    public long ContactId { get; set; }

    public List<Email> Emails { get; set; }
}

public class Email
{
    [EmailAddress]
    public string email { get; set; }
}