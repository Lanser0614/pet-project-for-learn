using Microsoft.EntityFrameworkCore.ChangeTracking;
using treni_contact.Models;
using treni_contact.Models.Contact;

namespace treni_contact.Repositories;

public interface IContactRepository
{
    // public Task<List<StudentDetails>> GetStudentListAsync();
    // public Task<StudentDetails> GetStudentByIdAsync(int id);
    public Task<Contact> AddContactAsync(Contact contactCreateDto);
    // public Task<int> UpdateStudentAsync(StudentDetails studentDetails);
    // public Task<int> DeleteStudentAsync(int id);
}