using SGXC.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGXC.Services
{
    class RunnerServices : IRunnerServices
    {
        readonly ISQLite dbservices;

        public SQLiteAsyncConnection Connection { get; set; }
        public ITimeServices TimeServices { get; set; }

        public RunnerServices(ISQLite dbservices, ITimeServices timeServices)
        {
            this.dbservices = dbservices;
            this.Connection = dbservices.GetConnection();
            this.TimeServices = timeServices;
        }

        public async Task<List<Runner>> GetAllRunners()

        {
            return await Connection.Table<Runner>().ToListAsync();
        }


        public async Task<int> SaveRunner(Runner runner)

        {
            var er = await Connection.Table<Runner>().Where(r => r.Id == runner.Id).FirstOrDefaultAsync();
            if (er == null)
            {
                return await Connection.InsertAsync(runner);
            }
            return await Connection.InsertOrReplaceAsync(runner);

        }

        public async Task AddToRunner(Runner runner)

        {
            await Connection.UpdateWithChildrenAsync(runner);
        }

        public async Task<List<Runner>> GetRunnersByEventID(int eventid)
        {
            var listofrunners = new List<Runner>();
            var runners = await Connection.Table<RunnerEvent>().Where(e => e.EventId == eventid).ToListAsync();
            foreach (var runner in runners)
            {
                var eachrunner = await TimeServices.GetRunner(runner.RunnerId);
                listofrunners.Add(eachrunner);
            }
            return listofrunners;

        }

        public async Task DeleteRunner(Runner runner)
        {
            List<Time> timestodelete = await Connection.Table<Time>().Where(x => x.RunnerId == runner.Id).ToListAsync();
            foreach (var time in timestodelete)
            {
                await Connection.DeleteAsync<Time>(time.Id);
            }
            await Connection.DeleteAsync(runner, recursive: true);
            await Connection.UpdateWithChildrenAsync(runner);
        }
    }
}
