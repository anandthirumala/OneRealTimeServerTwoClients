using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace Clients.ConsoleTwo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = new HostBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .ConfigureServices((services) =>
                {
                    services.AddHostedService<PrintHubClient>();
                })
                .Build();

            host.Run();
        }
    }
}
