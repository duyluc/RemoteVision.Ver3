using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Ultilities
{
    public static class Log
    {

        public static string LogFilePath = System.IO.Path.Combine(@"C:\users\" + Environment.MachineName + @"\Desktop\LogFile");

        public static string WriteLog(Exception _t)
        {
            string message = "";
            if (!Directory.Exists(LogFilePath))
            {
                Directory.CreateDirectory(LogFilePath);
            }
            using (StreamWriter file = new StreamWriter(Path.Combine(LogFilePath, DateTime.Now.ToString("dd-MM-yyy") + ".txt")))
            {
                message = DateTime.Now.ToString("--> HH:mm:ss -->") + _t.Source.ToString() + $"<-- {_t.Message}";
                file.WriteLine(message);
            }
            return message;
        }

        public static void WriteLog(string _t)
        {
            if (!Directory.Exists(LogFilePath))
            {
                Directory.CreateDirectory(LogFilePath);
            }
            using (StreamWriter file = new StreamWriter(Path.Combine(LogFilePath, DateTime.Now.ToString("dd-MM-yyy") + ".txt")))
            {
                file.WriteLine(_t);
            }
        }
    }
}
