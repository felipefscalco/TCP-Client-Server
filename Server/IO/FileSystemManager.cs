using Server.Abstractions;
using System.Collections.Generic;
using System.IO;

namespace Server.IO
{
    public sealed class FileSystemManager : IFileSystemManager
    {
        public Stream CreateFile(string path)
            => new FileStream(path, FileMode.Create);

        public void CreateDirectory(string path)
            => Directory.CreateDirectory(path);

        public bool FileExists(string path)
            => File.Exists(path);

        public string GetDirectoryName(string path)
            => Path.GetDirectoryName(path);

        public bool DirectoryExists(string path)
            => Directory.Exists(path);

        public IEnumerable<string> GetAllDirectoryFiles(string path)
            => Directory.GetFiles(path, "*", SearchOption.AllDirectories);

        public void DeleteDirectory(string directory)
            => Directory.Delete(directory, true);

        public void DeleteFile(string path)
            => File.Delete(path);
    }
}