using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Aceout.Web.Localization
{
    public class DirectoryManager : IDirectoryManager
    {
        private readonly FileSystemWatcher _watcher;
        public event FileSystemEventHandler Changed
        {
            add
            {
                _watcher.Changed += value;
            }
            remove
            {
                _watcher.Changed -= value;
            }
        }

        public string Path { get; }


        public DirectoryManager(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException(nameof(path));
            }

            Path = path;
            _watcher = new FileSystemWatcher(Path);
        }

        public string[] GetFiles()
        {
            return Directory.GetFiles(Path)
                .Select(System.IO.Path.GetFileName)
                .ToArray();
        }

        public string ReadFile(string fileName)
        {
            var path = System.IO.Path.Combine(Path, fileName);

            StringBuilder builder;
            using (var sr = File.OpenText(path))
            {
                builder = new StringBuilder((int)(sr.BaseStream.Length / 2));
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    builder.Append(str);
                }
            }

            return builder.ToString();
        }

    }
}
