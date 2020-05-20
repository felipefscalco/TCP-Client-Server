using System.Collections.Generic;
using System.IO;

namespace Data.Sqlite.IO.Interfaces
{
    public interface IFileSystemManager
    {
        Stream CreateFile(string path);

        string GetDirectoryName(string path);

        void CreateDirectory(string path);

        bool FileExists(string path);

        bool DirectoryExists(string path);

        IEnumerable<string> GetAllDirectoryFiles(string path);

        void DeleteDirectory(string directory);

        void DeleteFile(string path);
    }
}