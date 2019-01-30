using System;
using System.Threading.Tasks;
using HubServiceInterfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace Clients.ConsoleTwo
{
    public class ClockHubClient : IClock
    {
        private readonly ILogger<ClockHubClient> _logger;
        private HubConnection _connection;

        public ClockHubClient(ILogger<ClockHubClient> logger)
        {
            _logger = logger;
            
            _connection = new HubConnectionBuilder()
                .WithUrl(Strings.HubUrl)
                .Build();

            _connection.On<DateTime>(Strings.Events.TimeSent, 
                (dateTime) => ShowTime(dateTime));

            _connection.StartAsync().Wait();
        }

        public void ShowTime(DateTime currentTime)
        {
            _logger.LogInformation($"{currentTime.ToShortTimeString()}");
        }
    }
}