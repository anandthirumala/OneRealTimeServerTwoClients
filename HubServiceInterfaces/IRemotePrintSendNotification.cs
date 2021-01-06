using System.Threading.Tasks;

namespace HubServiceInterfaces
{
    public interface IRemotePrintSendNotification
    {
        Task PrintResponseAsync(string printId, PrintStatus status);
    }
}
