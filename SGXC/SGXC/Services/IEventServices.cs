using SGXC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGXC.Services
{
    public interface IEventServices
    {
        Task<List<Event>> GetAllEvents();
        Task<Event> GetEventById(int id);
        Task<int> GetNotRanEvents();
        Task<int> CreateEvent(Event @event);
        Task AddRunnersToEvent(Event @event, List<Runner> runners);
        Task AddToEvent(Event @event);
        Task DeleteEvent(Event @event);
        Task UpdateRunnersToEvent(Event @event, List<Runner> runners);
        Task UpdateEvent(Event @event);
    }
}
