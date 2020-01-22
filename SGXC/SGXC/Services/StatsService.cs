using SGXC.Models;
using SGXC.Models.Helper;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGXC.Services
{
    class StatsService : IStatsService
    {
        private readonly ISQLite dbservices;

        public SQLiteAsyncConnection Connection { get; set; }
        public IEventServices EventServices { get; set; }

        public StatsService(ISQLite dbservices, IEventServices eventServices)
        {
            this.dbservices = dbservices;
            Connection = dbservices.GetConnection();
            EventServices = eventServices;
        }

        public async Task<List<PracticeData>> GetPracticeData(int runnerid)
        {
            //Select events that are practices;
            var listofpractices = new List<Event>();            
            var events = await Connection.Table<RunnerEvent>().Where(r => r.RunnerId == runnerid).ToListAsync();            
            foreach (var @event in events)
            {
                var practiceevent = await EventServices.GetEventById(@event.EventId);
                if (!practiceevent.IsRace && practiceevent.IsRan)
                {
                    listofpractices.Add(practiceevent);
                }
            }



            throw new NotImplementedException();
        }

        public async Task<List<RaceData>> GetRaceData(int runnerid)
        {
            throw new NotImplementedException();
        }
    }
}
