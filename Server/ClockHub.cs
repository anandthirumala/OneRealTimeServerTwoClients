using System;
using HubServiceInterfaces;
using Microsoft.AspNetCore.SignalR;

namespace Server
{
    public class ClockHub : Hub<IClock>
    {
        public void SendTimeToClients(DateTime dateTime)
        {
            Clients.All.ShowTime(dateTime);
        }
    }
}