using System.Threading;
using System.Threading.Tasks;

namespace HubServiceInterfaces
{
    public interface IRemotePrintHubClient
    {
        Task InitialiseReceiverAsync(IRemotePrintReceiveNotification receiveNotification, CancellationToken cancellationToken);
        Task InitialiseSenderAsync(IRemotePrintSendNotification sendNotification, CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}