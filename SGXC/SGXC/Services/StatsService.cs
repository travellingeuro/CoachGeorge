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
            var practicedata = new List<PracticeData>();
            //Get times by runner 
            var listoftimes = await TimeServices.GetListOfTimesbyRunnerId(runnerid);

            //Creates list of dates
            Dictionary<int, DateTime> eventdates = new Dictionary<int, DateTime>();
            

            //Select times from a events
            var practices = listoftimes.GroupBy(ev => new { ev.EventId }).Select(grp => grp.First()).ToList();
            foreach (var practice in practices)
            {
                var tmpevent=await EventServices.GetEventById(practice.EventId.GetValueOrDefault());
                //if event is a race remove form the list of races
                if (tmpevent.IsRace)
                {
                    listoftimes.RemoveAll(e => e.EventId == practice.EventId.GetValueOrDefault());
                }
                else
                //if event is not a race creates a dictionary with Key the event Id and content the race date to use later
                {
                    eventdates.Add(tmpevent.Id, tmpevent.RaceDate);
                    
                }
            }

            //creates PracticeData
            var grouprun = listoftimes.GroupBy(d => d.Distance).ToList();
                       

            foreach (var group in grouprun)
            {
                var practiceresults = new List<PracticeResult>();

                foreach (var run in group)
                {
                    
                    var practiceresult = new PracticeResult { MaxTime=run.Times.TotalMilliseconds, MinTime=run.Times.TotalMilliseconds,Date = eventdates[run.EventId.GetValueOrDefault()] };                    
                    practiceresults.Add(practiceresult);
                }

                //Select the lower and higher times if there is more than one for a given distance i n a day
                var tmpresults=practiceresults.GroupBy(d => d.Date).ToList();
                foreach (var date in tmpresults)
                {
                    if (date.Count()>1)
                    {
                        var maxtime=date.Max(t => t.MaxTime);
                        var mintime = date.Min(t => t.MaxTime);
                        var practiceresult = new PracticeResult { Date = date.Key, MaxTime = maxtime, MinTime = mintime };
                        //erase the various entries with same date and distance
                        practiceresults.RemoveAll(d => d.Date == date.Key);
                        //add a single entry with the maxtime and mintime of the day
                        practiceresults.Add(practiceresult);
                    }
                }

                //Add practices results to each distance
                var practice = new PracticeData { Distance = group.Key, PracticeResults= practiceresults };
                practicedata.Add(practice);
            }

            return practicedata;

        }


        public async Task<List<RaceData>> GetRaceData(int runnerid)
        {
            var racedata = new List<RaceData>();
            //Get times by runner 
            var listoftimes = await TimeServices.GetListOfTimesbyRunnerId(runnerid);

            //Creates list of dates
            Dictionary<int, DateTime> eventdates = new Dictionary<int, DateTime>();
            Dictionary<int, string> eventseason = new Dictionary<int, string>();

            //Select times from a event 
            var practices = listoftimes.GroupBy(ev => new { ev.EventId }).Select(grp => grp.First()).ToList();
            foreach (var practice in practices)
            {
                var tmpevent = await EventServices.GetEventById(practice.EventId.GetValueOrDefault());
                //if event is not a race remove form the list of races
                if (!tmpevent.IsRace)
                {
                    listoftimes.RemoveAll(e => e.EventId == practice.EventId.GetValueOrDefault());
                }
                else
                //if event is a race creates a dictionary with Key the event Id and content the race date to use later
                {
                    eventdates.Add(tmpevent.Id, tmpevent.RaceDate);
                    eventseason.Add(tmpevent.Id, tmpevent.Season);
                }
            }
            //Creates RaceData
            //Group list of times by Season

            
            var groupseason = eventseason.GroupBy(d => d.Value);           

            foreach (var group in groupseason)
            {
                var raceresults = new List<RaceResult>();

               foreach (var id in group)
                {
                    var racesplits = listoftimes.Where(e => e.EventId == id.Key).ToList();
                    var raceresult = new RaceResult { Date = eventdates[id.Key], Time = racesplits.Last().Times.TotalMilliseconds, 
                                                        Distance = racesplits.Last().Distance };
                    raceresults.Add(raceresult);
                }
                var races = new RaceData { Season = group.Key, RaceResults = raceresults };
                racedata.Add(races);
            }

            return racedata;
        }


    }
}
