using treni_contact.Configs.DataBase;
using treni_contact.Models.Contact;

namespace treni_contact.Tasks;

public class NotificationBirtDayTask : BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromSeconds(5);
    
    private readonly IServiceProvider serviceProvider;

    public NotificationBirtDayTask(IServiceProvider _serviceProvider)
    {
        serviceProvider = _serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromDays(1));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await Task.Run(() =>
            {
                var scope = serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var contacts = dbContext.contacts
                    .Where(x => x.BirthDay == DateTime.Today)
                    .ToList();

                foreach (var var in contacts)
                {
                    Console.WriteLine(var.FirstName);
                }

                return Task.CompletedTask;
            }, stoppingToken);
        }
    }
    
}