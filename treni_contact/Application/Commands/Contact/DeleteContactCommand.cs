using System.Net;
using MediatR;
using treni_contact.Configs.DataBase;
using treni_contact.Exceptions;

namespace treni_contact.Application.Commands.Contact;

public class DeleteContactCommand : IRequest<HttpStatusCode>
{
    public DeleteContactCommand(long id)
    {
        Id = id;
    }
    
    public long Id { get; set; }
}

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, HttpStatusCode>
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteContactCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<HttpStatusCode> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var contact = _dbContext.contacts.FirstOrDefault(c => c.Id == id);

        if (contact == null)
        {
            throw new DataBaseException("Not found");
        }

        _dbContext.contacts.Remove(contact);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return HttpStatusCode.NoContent;
    }
}