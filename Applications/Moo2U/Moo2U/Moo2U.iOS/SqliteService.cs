namespace Moo2U.iOS {
    using System;
    using System.IO;
    using Moo2U.Services;
    using SQLite.Net;
    using SQLite.Net.Async;
    using SQLite.Net.Platform.XamarinIOS;

    /// <summary>
    ///     Class SqliteService.
    /// </summary>
    /// <seealso cref="Moo2U.Services.ISQLite" />
    /// <remarks>
    ///     <p>The Unity container ensures there is only one instance of this class.</p>
    ///     <p>For performance reasons, connections are single instance as well.</p>
    /// </remarks>
    public class SqliteService : ISQLite {

        readonly String _databasePath;
        SQLiteAsyncConnection _sqLiteAsyncConnection;
        SQLiteConnection _sqLiteConnection;

        public SqliteService() {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            _databasePath = Path.Combine(documentsPath, Constants.DatabaseName);

        }

        public void CreateDatabase() {
            if (File.Exists(_databasePath)) {
                File.Delete(_databasePath);
            }
            File.Create(_databasePath);
        }

        public SQLiteAsyncConnection GetAsyncConnection() {
            return _sqLiteAsyncConnection ?? (_sqLiteAsyncConnection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(new SQLitePlatformIOS(), new SQLiteConnectionString(_databasePath, false))));
        }

        public SQLiteConnection GetConnection() {
            return _sqLiteConnection ?? (_sqLiteConnection = new SQLiteConnection(new SQLitePlatformIOS(), _databasePath, false));
        }

    }
}
