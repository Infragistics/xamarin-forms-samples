namespace Moo2U.Services {
    using SQLite.Net;
    using SQLite.Net.Async;

    public interface ISQLite {

        SQLiteAsyncConnection GetAsyncConnection();

        SQLiteConnection GetConnection();

        void CreateDatabase();

    }
}
