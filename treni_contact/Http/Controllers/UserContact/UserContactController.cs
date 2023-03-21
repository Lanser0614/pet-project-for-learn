using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using treni_contact.Configs.DataBase;
using treni_contact.Models.Entity.User;

namespace treni_contact.Http.Controllers.UserContact;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("/api/[controller]/[action]")]
public class UserContactController : ControllerBase
{
    public readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public UserContactController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        this._context = context;
        this._userManager = userManager;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var usersContact = _context.Users
            .Include(u => u.Contacts)
            .ThenInclude(contact => contact.Phones)
            .Include(u => u.Contacts)
            .ThenInclude(contact => contact.Emails)
        
            .ToList();
          
        return Ok(usersContact);
    }
}