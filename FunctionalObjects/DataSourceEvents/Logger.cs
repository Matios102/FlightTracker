using System;
using System.IO;

namespace FlightProject.FunctionalObjects.DaraSourceEvents
{
    public class Logger
    {
        private string logPath;

        public Logger()
        {
            string AllLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(AllLogPath))
                Directory.CreateDirectory(AllLogPath);


            DateTime now = DateTime.Now;
            string logFolderName = $"log_{now.ToString("dd_MM_yyyy")}";
            string logFileName = $"log_{now.ToString("HH_mm_ss")}.txt";

            string TodayLogPath = Path.Combine(AllLogPath, logFolderName);
            if (!Directory.Exists(TodayLogPath))
                Directory.CreateDirectory(TodayLogPath);

            logPath = Path.Combine(TodayLogPath, logFileName);
            string logMessage = $"LOGG {now.ToLongTimeString()}";
            LogChange(Environment.NewLine + logMessage + Environment.NewLine);
        }

        public void LogChange(string message)
        {
            File.AppendAllText(logPath, message + Environment.NewLine);
        }
    }
}
