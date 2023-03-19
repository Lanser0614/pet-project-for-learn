using MediatR;
using treni_contact.Configs.DataBase;
using treni_contact.Exceptions;
using treni_contact.Http.Requests.Contact;

namespace treni_contact.Application.Commands.Contact;

public class MassUpdateEmailForContactCommand : IRequest<int>
{
    public MassUpdateEmailForContactCommand(
        long contactId,
        List<EmailUpdate> emails
    )
    {
        ContactId = contactId;
        Email = emails;
    }
    public long ContactId { get; set; }
    public List<EmailUpdate> Email { get; set; }
}


public class MassUpdateEmailForContactCommandHandler : IRequestHandler<MassUpdateEmailForContactCommand, int>
{
    private readonly ApplicationDbContext _dbContext;

    public MassUpdateEmailForContactCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> Handle(MassUpdateEmailForContactCommand request, CancellationToken cancellationToken)
    {
        var id = request.ContactId;

        var contact = _dbContext.contacts.FirstOrDefault(c => c.Id == id);

        if (contact == null)
        {
            throw new DataBaseException("Not found");
        }

        var emails = request.Email;

        var emailIds = new List<long>();
        
        foreach (var value in emails)
        {
            emailIds.Add(value.id);
        }
        
        var existEmails = _dbContext.Email.Where(e => emailIds.Contains(e.id)).ToList();

        if (existEmails == null)
        {
            throw new DataBaseException("email is not have");
        }

        
        foreach (var value in emails)
        {
           var email = existEmails.FirstOrDefault(e => e.id == value.id);
           
           if (email?.Contact.Id != contact.Id)
           {
               throw new DataBaseException("Not found");
           }
           
           _dbContext.Entry(email).CurrentValues.SetValues(value);
        }

        try
        {
             await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            throw new DataBaseException(e.Message);
        }

        return existEmails.Count();

    }
}