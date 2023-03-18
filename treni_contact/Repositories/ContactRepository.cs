using Microsoft.EntityFrameworkCore.ChangeTracking;
using treni_contact.Configs.DataBase;
using treni_contact.Models;
using treni_contact.Models.Contact;

namespace treni_contact.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ContactRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Contact> AddContactAsync(Contact contactCreateDto)
    {
        var result = await _dbContext.contacts.AddAsync(contactCreateDto);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
            
    }
}