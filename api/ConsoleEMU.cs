using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace util
{
    public class ConsoleEMU
    {
        public static ILogger logger { get; private set; }
        public static void OpenNewConsole()
        {
            if (logger != null)
                return;
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            logger = factory.CreateLogger("server logger");
            logger.LogInformation("Hello World! Logging is {Description}.", "fun");
        }
        public static void WriteLine(object? value)
        {
            if (logger is null)
                return;
            logger.LogInformation($"{value.ToString()}\n");
        }
        public static void Write(object? value)
        {
            if (logger is null)
                return;
            logger.LogInformation(value.ToString());
        }
        public static void CloseNewConsole()
        {
            throw new NotImplementedException();
        }
    }
}
