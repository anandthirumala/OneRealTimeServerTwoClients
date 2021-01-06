using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace HubServiceInterfaces
{
    public class RemotePrintHubClient : IRemotePrintHubClient
    {
        private readonly ILogger<RemotePrintHubClient> _logger;
        private HubConnection _connection;

        public RemotePrintHubClient(ILogger<RemotePrintHubClient> logger)
        {
            _logger = logger;
        }

        public async Task InitialiseReceiverAsync(IRemotePrintReceiveNotification receiveNotification, CancellationToken cancellationToken)
        {
            _connection = Initialise();

            _connection.On<string>(
                nameof(receiveNotification.PrintRequestAsync),
                async printId => await receiveNotification.PrintRequestAsync(printId)
            );

            await StartAsync(cancellationToken);
        }

        public async Task InitialiseSenderAsync(IRemotePrintSendNotification sendNotification, CancellationToken cancellationToken)
        {
            _connection = Initialise();

            _connection.On<string, PrintStatus>(
                nameof(sendNotification.PrintResponseAsync),
                async (printId, status) => await sendNotification.PrintResponseAsync(printId, status)
            );

            await StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _connection.StopAsync(cancellationToken);
        }

        private HubConnection Initialise()
        {
            const string userName = "4093";
            const string syncHostPassword = "C8814A75-B4C2-40D3-AAF0-6F05AD558378";
            string basicAuthorizationHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{syncHostPassword}"));

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(Strings.HubUrl, options => options.Headers.Add("Authorization", $"Basic {basicAuthorizationHeader}"))
                .Build();

            connection.Closed += ConnectionClosed;

            return connection;
        }

        private async Task StartAsync(CancellationToken cancellationToken)
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
                    await Task.Delay(3000, cancellationToken);
                }
            }
        }

        private async Task ConnectionClosed(Exception ex)
        {
            _logger.LogInformation($"SignalR connection closed \n{ex.Message}\n");

            await StartAsync(CancellationToken.None);
        }

    }
}