using MediatR;
using treni_contact.Configs.Core.Contract;

namespace treni_contact.Configs.Services.MediatR;

public class MediatrService : IConfigureServiceInterface
{
    public void InstallServices(IServiceCollection service, IConfiguration configuration)
    {
        service.AddMediatR(typeof(Program));
    }
}