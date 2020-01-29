using SGXC.Models;
using SGXC.Models.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGXC.Services
{
    public interface ITimeServices
    {
        Task<int> CreateTimeList<T>(List<T> time);
        Task AddTime<T>(T time);
        Task UpdateTime<Time>(Time time);
        Task AddEventToTime<T>(T time);
        Task AddRunnerToTime<T>(T time);
        Task<List<EventWithRunners>> GetTimesByEventId(int EventId);
        Task<List<Time>> GetListOfTimesbyEventId(int EventId);
        Task<Runner> GetRunner(int RunnerId);
        Task<int> DeleteTime(string distance, int id);
        Task<int> DeleteNullEventIdTimes();
        Task<List<Time>> GetListOfTimesbyRunnerId(int RunnerId);
    }
}
