using SGXC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGXC.Services
{
    public interface IRunnerServices
    {
        Task<List<Runner>> GetAllRunners();

        Task<int> SaveRunner(Runner newrunner);

        Task AddToRunner(Runner runner);

        Task<List<Runner>> GetRunnersByEventID(int eventid);
        Task DeleteRunner(Runner runner);
    }
}
