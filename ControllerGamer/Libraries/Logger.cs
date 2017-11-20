using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace ControllerGamer.Libraries
{

    public static class Logger
    {
        public readonly static string LogPath; 
        private readonly static FileStream logfile;
        private readonly static StreamWriter logsw;

        public static event LogWindowHandler LogUpdate;

        static Logger()
        {
            if (!Directory.Exists(@"Logs"))
            {
                Directory.CreateDirectory(@"Logs");
            }
            LogPath = @"Logs\CG_" + DateTime.UtcNow.ToString("yyyy-MM-dd_HHmmss.ffff") + ".log";
            logfile = new FileStream(LogPath, FileMode.Append);
            logsw = new StreamWriter(logfile);
            logsw.AutoFlush = true;
            CleanLog();
        }

        private static void CleanLog()
        {
            if (Directory.Exists(@"Logs"))
            {
                string[] logs = Directory.GetFiles(@"Logs");
                foreach (string log in logs)
                {
                    DateTime dtlog = File.GetCreationTime(log);
                    DateTime dtnow = DateTime.Now;

                    TimeSpan delta = dtnow - dtlog;
                    if (delta.Hours > 24)
                    {
                        File.Delete(log);
                    }
                }
            }
        }


        public static void Log(string info)
        {
            info += "\r\n";
            LogUpdate(info);
            logsw.Write(info);
        }
        
        public static void Log(int info)
        {
            string info1 = info.ToString() + "\r\n";
            LogUpdate(info1);
            logsw.Write(info1);
        }


        public static void Log(object info)
        {
            string info1 = info.ToString()+ "\r\n";
            LogUpdate(info1);
            logsw.Write(info1);
        }

    }
}
