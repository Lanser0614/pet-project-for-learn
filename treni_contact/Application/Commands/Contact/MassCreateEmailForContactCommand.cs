using MediatR;
using treni_contact.Configs.DataBase;
using treni_contact.Exceptions;
using treni_contact.Http.Requests.Contact;

namespace treni_contact.Application.Commands.Contact;

public class MassCreateEmailForContactCommand : IRequest<int>
{
    public MassCreateEmailForContactCommand(
        long contactId,
        List<Email> emails
    )
    {
        ContactId = contactId;
        Emails = emails;
    }

    public long ContactId { get; set; }
    public List<Email> Emails { get; set; }
}

public class MassCreateEmailForContactCommandHandler : IRequestHandler<MassCreateEmailForContactCommand, int>
{
    private readonly ApplicationDbContext _dbContext;

    public MassCreateEmailForContactCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(MassCreateEmailForContactCommand request, CancellationToken cancellationToken)
    {
        var contact = _dbContext.contacts.FirstOrDefault(x => x.Id == request.ContactId);

        if (contact == null)
        {
            throw new DataBaseException("Not found");
        }

        var requestEmail = new List<string>();

        foreach (var var in request.Emails)
        {
            requestEmail.Add(var.email);
        }

        var existEmails = _dbContext.Email.Any(e => requestEmail.Contains(e.email));
        
        if (existEmails)
        {
            throw new DataBaseException("Email is exist");
        }


        List<Models.Entity.Email.Email> emails = new List<Models.Entity.Email.Email>();

        foreach (var var in request.Emails)
        {
            var phoneq = new Models.Entity.Email.Email()
            {
                email = var.email,
                created_at = DateTime.Now,
                Contact = contact
            };
            emails.Add(phoneq);
        }

        try
        {
            await _dbContext.Email.BulkInsertAsync(emails, cancellationToken);
        }
        catch (Exception e)
        {
            throw new DataBaseException(e.Message);
        }

        return emails.Count;
    }
}