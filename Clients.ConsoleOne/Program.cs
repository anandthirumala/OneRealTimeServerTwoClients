using System;
using System.Threading.Tasks;
using HubServiceInterfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace Clients.ConsoleOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl(Strings.HubUrl)
                .Build();

            connection.On<DateTime>(Strings.Events.TimeSent, (dateTime) => {
                Console.WriteLine(dateTime.ToString());
            });

            connection.StartAsync();
                
            Console.WriteLine("Client One listening. Hit Ctrl-C to quit.");
            Console.ReadLine();
        }
    }
}
