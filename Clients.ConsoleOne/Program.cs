using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Clients.ConsoleOne
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

                Console.WriteLine("Client One listening...");
                await host.RunAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Console.WriteLine("\n\nClient One closing, press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}