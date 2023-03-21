using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using treni_contact.Configs.DataBase;
using treni_contact.Exceptions;
using treni_contact.Http.Responses.Contact;
using treni_contact.Models.Entity.User;

namespace treni_contact.Application.Commands.Contact;

public class CreateContactCommand : IRequest<ContactCreateResponse>
{
    public CreateContactCommand(
        string firstName,
        string secondName,
        DateTime birthDay,
        string userName
    )
    {
        FirstName = firstName;
        SecondName = secondName;
        BirthDay = birthDay;
        UserName = userName;
    }

    public string FirstName { get; set; }
    public string SecondName { get; set; }
    
    public DateTime? BirthDay { get; set; }

    public string UserName { get; set; }


}

public static class CreateContactCommandExtension
{
    public static Models.Contact.Contact CreateContact(this CreateContactCommand command)
    {
        return new Models.Contact.Contact
            {
                FirstName = command.FirstName,
                SecondName = command.SecondName,
                BirthDay = command.BirthDay,
            }
            ;
    }
}

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ContactCreateResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateContactCommandHandler(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<ContactCreateResponse> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        

        var User = await _userManager.FindByNameAsync(request.UserName);
        var contact = request.CreateContact();
        contact.ApplicationUser = User;
        try
        {
            await _dbContext.contacts.AddAsync(contact, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            throw new DataBaseException(e.Message);
        }
        
        
        return new ContactCreateResponse(((long)contact.Id)!);
    }
}