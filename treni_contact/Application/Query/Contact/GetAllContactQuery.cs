using MediatR;
using Microsoft.EntityFrameworkCore;
using treni_contact.Configs.DataBase;
using treni_contact.Models.ViewModel;

namespace treni_contact.Application.Query.Contact;

public class GetAllContactQuery : IRequest<ContactCollectionViewModel>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
    public GetAllContactQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize < 1 ? 1 : pageSize;
    }
}

public class GetAllContactHandler : IRequestHandler<GetAllContactQuery, ContactCollectionViewModel>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllContactHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ContactCollectionViewModel> Handle(GetAllContactQuery request, CancellationToken cancellationToken)
    {
        var pagedData = await _dbContext.contacts
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Include(x => x.Phones)
            .Include(x => x.Emails)
            .ToListAsync(cancellationToken: cancellationToken);
        var totalRecords = await _dbContext.contacts.CountAsync(cancellationToken: cancellationToken);

        return new ContactCollectionViewModel
        {
            Contacts = pagedData,
            Count = totalRecords,
            PageSize = request.PageSize,
            PageNumber = request.PageNumber
        };
    }
}
