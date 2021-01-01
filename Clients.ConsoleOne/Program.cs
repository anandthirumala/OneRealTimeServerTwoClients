using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Clients.ConsoleOne
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