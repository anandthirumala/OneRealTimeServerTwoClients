using System.Threading;
using System.Threading.Tasks;
using HubServiceInterfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Clients.ConsoleOne
{
    public class PrintHubClient : IPrintNotification, IHostedService
    {
        private readonly ILogger<PrintHubClient> _logger;
        private readonly HubConnection _connection;

        public PrintHubClient(ILogger<PrintHubClient> logger)
        {
            _logger = logger;
            
            _connection = new HubConnectionBuilder()
                .WithUrl(Strings.HubUrl)
                .Build();

            _connection.On<string>(
                nameof(PrintRequestAsync), 
                async printId => await PrintRequestAsync(printId)
            );

            _connection.On<string, PrintStatus>(
                nameof(PrintResponseAsync), 
                async (printId, status) => await PrintResponseAsync(printId, status)
            );
        }

        public Task PrintRequestAsync(string printId)
        {
            _logger.LogInformation($"printId: {printId}");

            return Task.CompletedTask;
        }

        public Task PrintResponseAsync(string printId, PrintStatus status)
        {
            _logger.LogInformation($"printId: {printId}, status: {status}");

            return Task.CompletedTask;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    await _connection.StartAsync(cancellationToken);

                    break;
                }
                catch
                {
                    await Task.Delay(1000, cancellationToken);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _connection.StopAsync(cancellationToken);
        }

    }
}