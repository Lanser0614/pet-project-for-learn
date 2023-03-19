using treni_contact.Configs.Core.Contract;
using treni_contact.Tasks;

namespace treni_contact.Configs.Services.BackgroundTask;

public class BackGrroundTaskService : IConfigureServiceInterface
{
    public void InstallServices(IServiceCollection service, IConfiguration configuration)
    {
        service.AddHostedService<NotificationBirtDayTask>();
    }
}