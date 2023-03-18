using treni_contact.Configs.Core.Contract;
using treni_contact.Mapper;

namespace treni_contact.Configs.Services.AutoMapper;

public class AutoMapperService : IConfigureServiceInterface
{
    public void InstallServices(IServiceCollection service, IConfiguration configuration)
    {
        service.AddAutoMapper(typeof(AppMappingProfile));
    }
}