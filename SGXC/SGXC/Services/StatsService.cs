using SGXC.Models;
using SGXC.Models.Helper;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGXC.Services
{
    class StatsService : IStatsService
    {
        private readonly ISQLite dbservices;

        public SQLiteAsyncConnection Connection { get; set; }
        public IEventServices EventServices { get; set; }
        public ITimeServices TimeServices { get; set; }

        public StatsService(ISQLite dbservices, IEventServices eventServices, ITimeServices timeServices)
        {
            this.dbservices = dbservices;
            Connection = dbservices.GetConnection();
            EventServices = eventServices;
            TimeServices = timeServices;
        }

        public async Task<List<PracticeData>> GetPracticeData(int runnerid)
        {
            var distances = new List<string> { "100m", "200m", "300m", "400m (1/4 mile)", "600m" ,"800m (1/2 mile)",
                                                        "1K","1.2K","1.5K","1 mile","2K", "2 mile", "3 mile", "5K", "10K"};
            var PracticeData = new List<PracticeData>();

            //Select run practices of a runner;
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

            // Create Practice Data
            for (int i = 0; i < distances.Count; i++)
            {
                var data = new PracticeData { Distance = distances[i], PracticeResults = await Getlistof(listofpractices, distances[i]) };
                PracticeData.Add(data);
            }
            return PracticeData;

        }

        //Creates a list of practice result for each distance of every event
        private async Task<List<PracticeResult>> Getlistof(List<Event> listofpractices, string distance)
        {
            var PracticeResults = new List<PracticeResult>();
            foreach (var practice in listofpractices)
            {
                var listoftimes = await TimeServices.GetListOfTimesbyEventId(practice.Id);
                listoftimes.Select(d => d.Distance == distance);
                foreach (var time in listoftimes)
                {
                    var newpracticeresult = new PracticeResult { Date = practice.RaceDate, Time = time.Times };
                    PracticeResults.Add(newpracticeresult);
                }
            }
            return PracticeResults;
        }


        public async Task<List<RaceData>> GetRaceData(int runnerid)
        {
            throw new NotImplementedException();
        }
    }
}
