using SQLite;

namespace SGXC.Services
{
    public interface ISQLite
    {
        SQLiteAsyncConnection GetConnection();
        void CreateTables(SQLiteAsyncConnection sQLiteConnection);

        void DeleteAll(SQLiteAsyncConnection sQLiteConnection);
    }
}
