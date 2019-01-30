using HubServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Clients.ConsoleTwo
{
    class Program
    {
        public static void Main(string[] args)
        {
            new HostBuilder()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                })
                .ConfigureServices((services) => {
                    services.AddLogging();
                    services.AddSingleton<IClock>(new ClockHubClient(
                        services.BuildServiceProvider().GetRequiredService<ILogger<ClockHubClient>>()
                    ));
                })
                .Build()
                .Run();
        }
    }
}
