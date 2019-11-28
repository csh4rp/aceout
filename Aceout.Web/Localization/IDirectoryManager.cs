using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Aceout.Web.Localization
{
    public interface IDirectoryManager
    {
        string Path { get; }
        string[] GetFiles();
        string ReadFile(string fileName);
        event FileSystemEventHandler Changed;
    }
}
