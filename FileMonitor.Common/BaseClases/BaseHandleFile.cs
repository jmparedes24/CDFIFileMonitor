﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileMonitor.Common.BaseClases
{
    public class BaseHandleFile : IHandleFile
    {
        public virtual void GetFile()
        {
            throw new NotImplementedException();
        }

        public string[] GetFilesName(string pathFiles, List<string> typeFiles)
        {
            if (Directory.Exists(pathFiles))
            {
                var fileEntries = Directory.GetFiles(@"c:\CFDI\", "*.*", SearchOption.AllDirectories).Where(e => typeFiles.Contains(Path.GetExtension(e))).ToArray();
                foreach(var fileName in fileEntries)
                {
                    Console.WriteLine("Processed file '{0}'.", fileName);
                }
                return fileEntries;
            }

            return new string[] { };
        }

        public virtual void ReadFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public virtual IDictionary<string, string> GetInfoFile(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
