using SGXC.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGXC.Services
{
    public class EventServices : IEventServices
    {
        readonly ISQLite dbservices;
        public SQLiteAsyncConnection Connection { get; set; }
        public EventServices(ISQLite dbservices)
        {
            this.dbservices = dbservices;
            this.Connection = dbservices.GetConnection();
        }

        public async Task<int> CreateEvent(Event @event)
        {
            return await Connection.InsertAsync(@event);
        }

        public async Task AddRunnersToEvent(Event @event, List<Runner> runners)

        {
            @event.Participants = runners;
            await Connection.UpdateWithChildrenAsync(@event);
        }

        public async Task UpdateRunnersToEvent(Event @event, List<Runner> runners)
        {
            @event.Participants = runners;
            await Connection.InsertOrReplaceWithChildrenAsync(@event);
        }

        public async Task UpdateEvent(Event @event)
        {
            await Connection.UpdateAsync(@event);
        }

        public async Task<List<Event>> GetAllEvents()

        {
            return await Connection.Table<Event>().ToListAsync();
        }

        public async Task<Event> GetEventById(int id)
        {
            return await Connection.Table<Event>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }



        public async Task AddToEvent(Event @event)

        {
            await Connection.UpdateWithChildrenAsync(@event);
        }

        public async Task<int> GetNotRanEvents()

        {
            var notranevents = await Connection.Table<Event>().Where(r => r.IsRan == false).ToListAsync();
            var number = notranevents == null ? 0 : notranevents.Count();
            return number;
        }

        public async Task DeleteEvent(Event @event)
        {
            List<Time> timestodelete = await Connection.Table<Time>().Where(x => x.EventId == @event.Id).ToListAsync();
            foreach (var time in timestodelete)
            {
                await Connection.DeleteAsync<Time>(time.Id);
            }
            await Connection.DeleteAsync(@event, recursive: true);
            await Connection.UpdateWithChildrenAsync(@event);

        }

    }

}
