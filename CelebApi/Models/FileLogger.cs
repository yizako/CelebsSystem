using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CelebApi.Models
{
    public abstract class LogBase
    {
        public abstract void Log(string message);
    }

    public class FileLogger : LogBase
    {
        public string filePath = AppDomain.CurrentDomain.BaseDirectory + "/Log_"+DateTime.Now.ToString("yyyyMMdd")+".txt";
        public override void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }
    }
}