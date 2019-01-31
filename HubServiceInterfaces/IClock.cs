using System;
using System.Threading.Tasks;

namespace HubServiceInterfaces
{
    public interface IClock
    {
        Task ShowTime(DateTime currentTime);
    }
}
