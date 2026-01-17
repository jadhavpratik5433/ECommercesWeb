

using Serilog;

namespace Ecommerces.sherd.Logs
{
    public static class LogExpception
    {
        public static void LogExpceptions(Exception ex)
        {
            LogToFile(ex.Message);
            LogToConsole(ex.Message);
            LogToDebugger(ex.Message);
        }
        public static void LogToFile(string message) => Log.Information(message);
        public static void LogToConsole(string message) => Log.Information(message);
        public static void LogToDebugger(string message) => Log.Information(message);


        // Implementation for logging to a file

    }
}
