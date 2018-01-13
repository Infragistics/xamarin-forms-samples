namespace Moo2U.Services {
    using System;
    using SQLite;

    public interface ISQLiteConnectionService {

        SQLiteAsyncConnection GetAsyncConnection();

        SQLiteConnection GetConnection();

    }
}
