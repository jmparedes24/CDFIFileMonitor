using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileMonitor.Common
{
    public interface IHandleFile
    {
        void ReadFile(string fileName);

        IDictionary<string, string> GetInfoFile(string fileName);

        string[] GetFilesName(string pathFiles, List<string> typeFiles);

        void GetFile();

        void MoveFile(string source, string destination);
    }
}
