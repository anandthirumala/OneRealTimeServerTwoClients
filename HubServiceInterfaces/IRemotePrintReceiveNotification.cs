using System.Threading.Tasks;

namespace HubServiceInterfaces
{
    public interface IRemotePrintReceiveNotification
    {
        Task PrintRequestAsync(string printId);
    }
}
