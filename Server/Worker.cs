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
        private IHubContext<ClockHub> _clockHub;

        public Worker(ILogger<Worker> logger, IHubContext<ClockHub> clockHub)
        {
            _logger = logger;
            _clockHub = clockHub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: {DateTime.Now}");
                await _clockHub.Clients.All.SendAsync(Strings.Events.TimeSent, DateTime.Now);
                await Task.Delay(1000);
            }
        }
    }
}