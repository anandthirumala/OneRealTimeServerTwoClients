using System;
using System.Threading;
using System.Threading.Tasks;
using HubServiceInterfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Server
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<PrintHub, IRemotePrintNotification> printHub;

        public Worker(ILogger<Worker> logger, IHubContext<PrintHub, IRemotePrintNotification> printHub)
        {
            _logger = logger;
            this.printHub = printHub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: {DateTime.Now}");

                var printId = Guid.NewGuid().ToString();

                await printHub.Clients.All.PrintRequestAsync(printId);
                await Task.Delay(250, stoppingToken);

                await printHub.Clients.All.PrintResponseAsync(printId, PrintStatus.Success);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}