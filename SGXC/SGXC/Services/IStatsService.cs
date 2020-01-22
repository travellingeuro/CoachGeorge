using SGXC.Models.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGXC.Services
{
    public interface IStatsService
    {
        Task <List<RaceData>> GetRaceData(int runnerid);
        Task<List<PracticeData>> GetPracticeData(int runnerid);
    }
}
