using System.Globalization;
using MediatR;
using treni_contact.Configs.DataBase;
using treni_contact.Exceptions;
using treni_contact.Http.Requests.Contact;

namespace treni_contact.Application.Commands.Contact;

public class MassCreatePhoneCommand : IRequest<int>
{
    public MassCreatePhoneCommand(
        long contactId,
        List<Phone> phones
    )
    {
        ContactId = contactId;
        Phones = phones;
    }

    public long ContactId { get; set; }
    public List<Phone> Phones { get; set; }
}

public class MassCreatePhoneHandler : IRequestHandler<MassCreatePhoneCommand, int>
{
    private readonly ApplicationDbContext _dbContext;

    public MassCreatePhoneHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(MassCreatePhoneCommand request, CancellationToken cancellationToken)
    {
        var contact = _dbContext.contacts.FirstOrDefault(x => x.Id == request.ContactId);
        
        if (contact == null)
        {
            throw new DataBaseException("Not found");
        }

        var requestPhone = new List<long>();

        foreach (var var in request.Phones)
        {
            requestPhone.Add(var.phone);
        }

        var existPhones = _dbContext.Phone.Any(e => requestPhone.Contains(e.phone));

        if (existPhones)
        {
            throw new DataBaseException("Phone is exist");
        }

        List<Models.Entity.Phone.Phone> phone = new List<Models.Entity.Phone.Phone>();

        phone = GetPhonesList(request, contact, phone);

        try
        {
            await _dbContext.Phone.BulkInsertAsync(phone, cancellationToken);
        }
        catch (Exception e)
        {
            throw new DataBaseException(e.Message);
        }

        return phone.Count;
    }

    private List<Models.Entity.Phone.Phone> GetPhonesList(MassCreatePhoneCommand request, Models.Contact.Contact contact,
        List<Models.Entity.Phone.Phone> phone)
    {
        foreach (var var in request.Phones)
        {
            var phoneq = new Models.Entity.Phone.Phone()
            {
                phone = var.phone,
                created_at = DateTime.Now,
                Contact = contact
            };
            phone.Add(phoneq);
        }

        return phone;
    }
}