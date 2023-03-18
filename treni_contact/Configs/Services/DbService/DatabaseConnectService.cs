using Microsoft.EntityFrameworkCore;
using treni_contact.Configs.Core.Contract;
using treni_contact.Configs.DataBase;

namespace treni_contact.Configs.Services.DbService;

public class DatabaseConnectService : IConfigureServiceInterface
{
    public void InstallServices(IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("database");
        service.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
    }
}