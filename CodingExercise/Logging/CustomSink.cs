using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace CodingExercise.Logging
{
    public class CustomSink : ILogEventSink
    {
        public void Emit(LogEvent logEvent)
        {
            var result = logEvent.RenderMessage();

            Console.ForegroundColor = logEvent.Level switch
            {
                LogEventLevel.Debug => ConsoleColor.Green,
                LogEventLevel.Information => ConsoleColor.Blue,
                LogEventLevel.Error => ConsoleColor.Red,
                LogEventLevel.Warning => ConsoleColor.Yellow,
                _ => ConsoleColor.White,
            };
            Console.WriteLine($"{logEvent.Timestamp} - {logEvent.Level}: {result}");
        }
    }

    public static class CustomSinkExtensions
    {
        public static LoggerConfiguration CustomSink(this LoggerSinkConfiguration config)
        {
            return config.Sink(new CustomSink());
        }
    }


}
