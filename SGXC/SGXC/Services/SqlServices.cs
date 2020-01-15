using SGXC.Models;
using SQLite;
using System;
using System.IO;
namespace SGXC.Services
{
    class SqlServices : ISQLite
    {

        public SqlServices()
        {

        }

        public void DeleteAll(SQLiteAsyncConnection sQLiteConnection)
        {
            sQLiteConnection.DropTableAsync<Runner>().Wait();
            sQLiteConnection.DropTableAsync<Event>().Wait();
            sQLiteConnection.DropTableAsync<RunnerEvent>().Wait();
            sQLiteConnection.DropTableAsync<Time>().Wait();
            sQLiteConnection.DropTableAsync<Bib>().Wait();
        }

        public void CreateTables(SQLiteAsyncConnection sQLiteConnection)
        {
            sQLiteConnection.CreateTableAsync<Runner>().Wait();
            sQLiteConnection.CreateTableAsync<Event>().Wait();
            sQLiteConnection.CreateTableAsync<RunnerEvent>().Wait();
            sQLiteConnection.CreateTableAsync<Time>().Wait();
            sQLiteConnection.CreateTableAsync<Bib>().Wait();


            #region Ejemplo


            //var fakeevent = new Event() { EventName = "FinaleUCO", };
            //var runner1 = new Runner { Name = "Rufus" };
            //var runner2 = new Runner { Name = "Rogelio" };
            //var runner3 = new Runner { Name = "Brutus O" };

            //var fakerunners = new List<Runner> { runner1, runner2, runner3 };
            //fakeevent.Participants = fakerunners;
            //sQLiteConnection.InsertWithChildrenAsync(fakeevent,recursive:true);

            //var time1 = new Mile { Time = new TimeSpan(9, 5, 9) };
            //var time2 = new Mile { Time = new TimeSpan(6, 8, 9) };
            //var milestimes = new List<Mile> { time1, time2 };
            //fakeevent.MileTime = milestimes;
            //runner2.MileTime = milestimes;

            //sQLiteConnection.UpdateWithChildrenAsync(fakeevent);
            //sQLiteConnection.UpdateWithChildrenAsync(runner2);

            #endregion
        }

        public SQLiteAsyncConnection GetConnection()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "xcdb.db3");
            var db = new SQLiteAsyncConnection(dbPath);
            return db;
        }


    }
}
