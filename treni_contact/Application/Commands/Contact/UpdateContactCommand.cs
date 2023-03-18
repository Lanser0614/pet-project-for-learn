using MediatR;
using treni_contact.Configs.DataBase;
using treni_contact.Exceptions;
using treni_contact.Http.Responses.Contact;

namespace treni_contact.Application.Commands.Contact;

public class UpdateContactCommand : IRequest<ContactUpdateResponse>
{
    public UpdateContactCommand(
        string firstName,
        string secondName,
        int id
    )
    {
        FirstName = firstName;
        SecondName = secondName;
        Id = id;

    }

    public long Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
}

public static class UpdateContactCommandExtension
{
    public static Models.Contact.Contact CreateContact(this UpdateContactCommand command)
    {
        return new Models.Contact.Contact
            {
                Id = command.Id,
                FirstName = command.FirstName,
                SecondName = command.SecondName,
            }
            ;
    }
}

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ContactUpdateResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateContactCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContactUpdateResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var contact = _dbContext.contacts.FirstOrDefault(c => c.Id == id);

        if (contact == null)
        {
            throw new DataBaseException("Not found");
        }

        _dbContext.Entry(contact).CurrentValues.SetValues(request);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ContactUpdateResponse(id);
    }
}