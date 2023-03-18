namespace treni_contact.Configs.Core.Contract;

public interface IConfigureServiceInterface
{
    public void InstallServices(IServiceCollection service, IConfiguration configuration);
}