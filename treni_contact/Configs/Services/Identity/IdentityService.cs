using Microsoft.AspNetCore.Identity;
using treni_contact.Configs.Core.Contract;
using treni_contact.Configs.DataBase;
using treni_contact.Models.Entity.Role;
using treni_contact.Models.Entity.User;

namespace treni_contact.Configs.Services.Identity;

public class IdentityService : IConfigureServiceInterface
{
    public void InstallServices(IServiceCollection service, IConfiguration configuration)
    {
        // For Identity
        service.AddIdentity<ApplicationUser, ApplicationRole>(
                o =>
                {
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.User.RequireUniqueEmail = true;
                })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}