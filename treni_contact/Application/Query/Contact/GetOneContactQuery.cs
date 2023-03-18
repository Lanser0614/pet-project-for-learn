using MediatR;
using Microsoft.EntityFrameworkCore;
using treni_contact.Configs.DataBase;
using treni_contact.Exceptions;
using treni_contact.Models.ViewModel;

namespace treni_contact.Application.Query.Contact;

public class GetOneContactQuery : IRequest<Models.ViewModel.Contact>
{
    public long Id { get; set; }

    public GetOneContactQuery(long id)
    {
        Id = id;
    }
}

public class GetOneContactHandler : IRequestHandler<GetOneContactQuery, Models.ViewModel.Contact>
{
    private readonly ApplicationDbContext _dbContext;

    public GetOneContactHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Models.ViewModel.Contact> Handle(GetOneContactQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var contact = _dbContext.contacts.FirstOrDefault(c => c.Id == id);

        if (contact == null)
        {
            throw new DataBaseException("Not found");
        }

        return new Models
            .ViewModel
            .Contact(
                id,
                contact.FirstName,
                contact.SecondName
            );
    }
}