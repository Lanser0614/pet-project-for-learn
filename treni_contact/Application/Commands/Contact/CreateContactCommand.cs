using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using treni_contact.Configs.DataBase;
using treni_contact.Exceptions;
using treni_contact.Http.Responses.Contact;

namespace treni_contact.Application.Commands.Contact;

public class CreateContactCommand : IRequest<ContactCreateResponse>
{
    public CreateContactCommand(
        string firstName,
        string secondName
    )
    {
        FirstName = firstName;
        SecondName = secondName;
    }

    public string FirstName { get; set; }
    public string SecondName { get; set; }


}

public static class CreateContactCommandExtension
{
    public static Models.Contact.Contact CreateContact(this CreateContactCommand command)
    {
        return new Models.Contact.Contact
            {
                FirstName = command.FirstName,
                SecondName = command.SecondName,
            }
            ;
    }
}

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ContactCreateResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateContactCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContactCreateResponse> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = request.CreateContact();
        try
        {
            await _dbContext.contacts.AddAsync(contact, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            throw new DataBaseException(e.Message);
        }
        
        
        return new ContactCreateResponse((long)contact.Id);
    }
}