using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{
    public class LogFile : ILogFile
    {
        public string Path { get; }
        public string MyFile { get; }

        public StreamWriter SW { get; set; }

        public LogFile(string filename)
        {
            MyFile = filename;
            Path = Directory.GetCurrentDirectory() + @"\" + MyFile;
            if (!File.Exists(Path)) SW = File.CreateText(Path);
        }

        public void Log(string msg)
        {
            SW = File.AppendText(Path);
            SW.WriteLine(msg);
            SW.Close();
        }
    }
}
