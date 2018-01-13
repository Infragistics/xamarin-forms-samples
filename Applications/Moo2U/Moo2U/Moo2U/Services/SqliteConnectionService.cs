namespace Moo2U.Services {
    using System;
    using System.IO;
    using SQLite;

    public class SqliteConnectionService : ISQLiteConnectionService {

        readonly String _databasePath;
        SQLiteAsyncConnection _sqLiteAsyncConnection;
        SQLiteConnection _sqLiteConnection;

        public SqliteConnectionService(IFileHelper fileHelper) {
            if (fileHelper == null) {
                throw new ArgumentNullException(nameof(fileHelper));
            }

            _databasePath = fileHelper.GetLocalFilePath(Constants.DatabaseName);

            if (File.Exists(_databasePath)) {
                File.Delete(_databasePath);
            }

            File.Create(_databasePath);
        }

        public SQLiteAsyncConnection GetAsyncConnection() {
            return _sqLiteAsyncConnection ?? (_sqLiteAsyncConnection = new SQLiteAsyncConnection(_databasePath, false));
        }

        public SQLiteConnection GetConnection() {
            return _sqLiteConnection ?? (_sqLiteConnection = new SQLiteConnection(_databasePath, false));
        }

    }
}
