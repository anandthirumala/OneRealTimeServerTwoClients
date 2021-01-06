using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HubServiceInterfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Clients.ConsoleTwo
{
    public class RemotePrintSendService : IRemotePrintSendNotification, IHostedService
    {
        private readonly IRemotePrintHubClient remotePrintHubClient;
        private readonly ILogger<RemotePrintSendService> _logger;

        public RemotePrintSendService(IRemotePrintHubClient remotePrintHubClient, ILogger<RemotePrintSendService> logger)
        {
            this.remotePrintHubClient = remotePrintHubClient;
            _logger = logger;
        }

        public Task PrintResponseAsync(string printId, PrintStatus status)
        {
            _logger.LogInformation($"printId: {printId}, status: {status}");

            return Task.CompletedTask;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await remotePrintHubClient.InitialiseSenderAsync(this, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await remotePrintHubClient.StopAsync(cancellationToken);
        }
    }
}
