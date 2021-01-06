using System;
using System.Threading.Tasks;
using HubServiceInterfaces;
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
                        services.AddScoped<IRemotePrintHubClient, RemotePrintHubClient>();
                        services.AddHostedService<RemotePrintSendService>();
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