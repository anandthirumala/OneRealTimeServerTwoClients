using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Clients.ConsoleTwo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
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

                Console.WriteLine("Client Two listening...");
                await host.RunAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Console.WriteLine("\n\nClient Two closing, press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}