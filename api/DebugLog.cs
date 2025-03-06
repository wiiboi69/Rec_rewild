using start;
using System;

namespace api
{
    class DebugLog
    {
        public static void Log(string message, LogLevel level = LogLevel.Info)
        {
            if (Program.dev_log)
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                switch (level)
                {
                    case LogLevel.Info:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"[{timestamp}] [INFO] {message}");
                        break;
                    case LogLevel.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[{timestamp}] [WARNING] {message}");
                        break;
                    case LogLevel.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[{timestamp}] [ERROR] {message}");
                        break;
                }
                Console.ResetColor();
            }
        }

        public enum LogLevel
        {
            Info,
            Warning,
            Error
        }
    }
}
