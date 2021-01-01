using System.Threading.Tasks;

namespace HubServiceInterfaces
{
    public interface IPrintNotification
    {
        Task PrintRequestAsync(string printId);

        Task PrintResponseAsync(string printId, PrintStatus status);
    }
}
