using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HubServiceInterfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Clients.ConsoleOne
{
    public class RemotePrintReceiveService : IRemotePrintReceiveNotification, IHostedService
    {
        private readonly IRemotePrintHubClient remotePrintHubClient;
        private readonly ILogger<RemotePrintReceiveService> _logger;

        public RemotePrintReceiveService(IRemotePrintHubClient remotePrintHubClient, ILogger<RemotePrintReceiveService> logger)
        {
            this.remotePrintHubClient = remotePrintHubClient;
            _logger = logger;

        }

        public Task PrintRequestAsync(string printId)
        {
            _logger.LogInformation($"printId: {printId}");

            return Task.CompletedTask;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await remotePrintHubClient.InitialiseReceiverAsync(this, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await remotePrintHubClient.StopAsync(cancellationToken);
        }

    }
}