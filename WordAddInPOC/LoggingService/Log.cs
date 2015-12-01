using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Runtime.CompilerServices;
using System.IO;
using Serilog.Core;
using Serilog.Events;

namespace LogService
{
    public static class Log
    {
        static LoggingLevelSwitch loglevel = new LoggingLevelSwitch();
        public enum LogLevel
        {
            // Zusammenfassung:
            //     Specifies the meaning and relative importance of a log event.
            // Zusammenfassung:
            //     Anything and everything you might want to know about a running block of code.
            Verbose = 0,
            //
            // Zusammenfassung:
            //     Internal system events that aren't necessarily observable from the outside.
            Debug = 1,
            //
            // Zusammenfassung:
            //     The lifeblood of operational intelligence - things happen.
            Information = 2,
            //
            // Zusammenfassung:
            //     Service is degraded or endangered.
            Warning = 3,
            //
            // Zusammenfassung:
            //     Functionality is unavailable, invariants are broken or data is lost.
            Error = 4,
            //
            // Zusammenfassung:
            //     If you have a pager, it goes off when one of these occurs.
            Fatal = 5,

        }

        public static void InitLog()
        {
#if DEBUG
            loglevel.MinimumLevel = LogEventLevel.Debug;
            Serilog.Log.IsEnabled(LogEventLevel.Debug);
#else
           loglevel.MinimumLevel = LogEventLevel.Information;
#endif
            Serilog.Log.Logger = new LoggerConfiguration().WriteTo.RollingFile(@"Log\logfile-{Date}.txt",
               outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] [{SourceFile}] [{SourceMember}] [{SourceLine}] {Message}{NewLine}{Exception}", retainedFileCountLimit: 20)
               .MinimumLevel.ControlledBy(loglevel)
                .CreateLogger();

        }
        public static void SetLogLevel(LogLevel level)
        {
            string sLvl = level.ToString();
            loglevel.MinimumLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), sLvl);
        }
        public static void Warning(string messageTemplate, CallerInfo callerInfo, params object[] propertyValues)
        {
            Serilog.Log.Logger
                .ForHere(callerInfo.CallerFilePath, callerInfo.CallerMemberName, callerInfo.CallerLineNumber)
                .Warning(messageTemplate, propertyValues);
        }

        public static void Information(string messageTemplate, CallerInfo callerInfo, params object[] propertyValues)
        {
            Serilog.Log.Logger
                .ForHere(callerInfo.CallerFilePath, callerInfo.CallerMemberName, callerInfo.CallerLineNumber)
                .Information(messageTemplate, propertyValues);
        }
        public static void Debug(string messageTemplate, CallerInfo callerInfo, params object[] propertyValues)
        {
            Serilog.Log.Logger
                .ForHere(callerInfo.CallerFilePath, callerInfo.CallerMemberName, callerInfo.CallerLineNumber)
                .Debug(messageTemplate, propertyValues);
        }
        public static void Verbose(string messageTemplate, CallerInfo callerInfo, params object[] propertyValues)
        {
            Serilog.Log.Logger
                .ForHere(callerInfo.CallerFilePath, callerInfo.CallerMemberName, callerInfo.CallerLineNumber)
                .Verbose(messageTemplate, propertyValues);
        }
        public static void Error(string messageTemplate, CallerInfo callerInfo, params object[] propertyValues)
        {
            Serilog.Log.Logger
                .ForHere(callerInfo.CallerFilePath, callerInfo.CallerMemberName, callerInfo.CallerLineNumber)
                .Error(messageTemplate, propertyValues);
        }

        public static void Error(Exception ex, CallerInfo callerInfo, string p, params object[] propertyValues)
        {
            Serilog.Log.Logger.ForHere(callerInfo.CallerFilePath, callerInfo.CallerMemberName, callerInfo.CallerLineNumber)
                .Error(ex, p, propertyValues);
        }
    }

    public static class LoggerExtensions
    {
        public static ILogger ForHere(
            this ILogger logger,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            return logger
                .ForContext("SourceFile", callerFilePath)
                .ForContext("SourceMember", callerMemberName)
                .ForContext("SourceLine", callerLineNumber);
        }
    }

    public class CallerInfo
    {
        public string CallerFilePath { get; private set; }

        public string CallerMemberName { get; private set; }

        public int CallerLineNumber { get; private set; }

        private CallerInfo(string callerFilePath, string callerMemberName, int callerLineNumber)
        {
            this.CallerFilePath = callerFilePath;
            this.CallerMemberName = callerMemberName;
            this.CallerLineNumber = callerLineNumber;
        }

        public static CallerInfo Create(
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            return new CallerInfo(Path.GetFileName(callerFilePath), callerMemberName, callerLineNumber);
        }
    }
}
