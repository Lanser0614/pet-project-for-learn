using treni_contact.Configs.Core.Contract;
using treni_contact.Repositories;

namespace treni_contact.Configs.Services.AddScope;

public class AddScopeService : IConfigureServiceInterface
{
    public void InstallServices(IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped<IContactRepository, ContactRepository>();
    }
}