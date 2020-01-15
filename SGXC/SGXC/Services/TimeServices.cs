using SGXC.Models;
using SGXC.Models.Helper;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGXC.Services
{
    public class TimeServices : ITimeServices

    {
        readonly ISQLite dbservices;

        public SQLiteAsyncConnection Connection { get; set; }

        public TimeServices(ISQLite dbservices)
        {
            this.dbservices = dbservices;
            this.Connection = dbservices.GetConnection();
        }
        public async Task<int> CreateTimeList<T>(List<T> times)
        {
            return await Connection.InsertAllAsync(times);
        }

        public async Task AddTime<T>(T time)
        {
            await Connection.InsertAsync(time);
        }

        public async Task UpdateTime<Time>(Time time)
        {
            await Connection.UpdateAsync(time);
        }

        public async Task AddEventToTime<T>(T time)
        {
            await Connection.UpdateWithChildrenAsync(time);
        }
        public async Task AddRunnerToTime<T>(T time)
        {
            await Connection.UpdateWithChildrenAsync(time);
        }

        public async Task<List<EventWithRunners>> GetTimesByEventId(int EventId)

        {
            List<EventWithRunners> eventWithRunnersList = new List<EventWithRunners>();

            List<Time> times = await Connection.Table<Time>().Where(e => e.EventId == EventId).ToListAsync(); //list of time

            foreach (var time in times)
            {
                var eventwithrunner = new EventWithRunners()
                {
                    Runner = await GetRunner(time.RunnerId),
                    RanTime = time
                };
                eventWithRunnersList.Add(eventwithrunner);
            }

            var order = eventWithRunnersList.GroupBy(d => (d.RanTime.Distance, d.RanTime.Repetitions)).ToList();

            foreach (var ele in order)
            {
                foreach (var k in ele)
                {
                    var d = ele.Key.Distance;
                    var r = ele.Key.Repetitions;
                    k.Category = string.Format("{0}, lap # {1}",
                         d, r);
                }

            }

            eventWithRunnersList = order.SelectMany(d => d).ToList();


            return eventWithRunnersList;
        }

        public async Task<List<Time>> GetListOfTimesbyEventId(int EventId)

        {
            List<Time> listoftime = await Connection.Table<Time>().Where(e => e.EventId == EventId).ToListAsync();
            return listoftime;
        }


        public async Task<int> DeleteTime(string d, int e)
        {
            List<Time> timestodelete = await Connection.Table<Time>().Where(x => x.EventId == e).Where(y => y.Distance == d).ToListAsync();

            foreach (var time in timestodelete)
            {
                await Connection.DeleteAsync<Time>(time.Id);

            }
            return 0;
        }

        public async Task<int> DeleteNullEventIdTimes()

        {
            List<Time> timestodelete = await Connection.Table<Time>().Where(e => e.EventId == null).ToListAsync();


            foreach (var time in timestodelete)
            {
                await Connection.DeleteAsync(time);
            }
            return 0;
        }

        public async Task<Runner> GetRunner(int RunnerId)
        {
            var runner = await Connection.Table<Runner>().Where(r => r.Id == RunnerId).FirstOrDefaultAsync();

            return runner;
        }


    }
}
