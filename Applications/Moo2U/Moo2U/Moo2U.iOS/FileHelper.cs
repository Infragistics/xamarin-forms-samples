namespace Moo2U.iOS {
    using System;
    using System.IO;
    using Moo2U.Services;

    public class FileHelper : IFileHelper {

        public String GetLocalFilePath(String filename) {
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder)) {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }

    }
}
