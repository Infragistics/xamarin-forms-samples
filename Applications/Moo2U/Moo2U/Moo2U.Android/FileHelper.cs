namespace Moo2U.Droid {
    using System;
    using System.IO;
    using Moo2U.Services;

    public class FileHelper : IFileHelper {

        public String GetLocalFilePath(String filename) {
            if (String.IsNullOrWhiteSpace(filename)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(filename));
            }

            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

    }
}
