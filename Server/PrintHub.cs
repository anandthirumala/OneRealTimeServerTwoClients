using System;
using System.Threading.Tasks;
using HubServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Server
{
    [Authorize]
    public class PrintHub : Hub<IRemotePrintNotification>
    {
        private readonly ILogger<PrintHub> logger;

        public PrintHub(ILogger<PrintHub> logger)
        {
            this.logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            logger.LogInformation($"Client connected Branch {Context.UserIdentifier} ConnectionID {Context.ConnectionId}");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            logger.LogInformation($"Client disconnected Branch {Context.UserIdentifier} ConnectionID {Context.ConnectionId}");

            return base.OnDisconnectedAsync(exception);
        }
    }
}
