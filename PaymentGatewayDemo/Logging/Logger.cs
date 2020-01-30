/// ----------------------------------------------------------------------
/// <summary>
/// Defines the Logger class.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Logging
{
    using Serilog;
    using System;
    using System.Configuration;

    /// <summary>
    /// Logger
    /// </summary>
    public class Logger : ILogger
    {
        private readonly Serilog.ILogger logger = null;
        private string connectionString;
        private string logTable;

        /// <summary>
        /// Logger constructor
        /// </summary>
        public Logger()
        {
            this.connectionString = ConfigurationManager.AppSettings["serilog:write-to:MSSqlServer.connectionString"];
            this.logTable = ConfigurationManager.AppSettings["serilog:write-to:MSSqlServer.tableName"];

            this.logger = new LoggerConfiguration().WriteTo.MSSqlServer(
                this.connectionString,
                this.logTable).CreateLogger();
            Serilog.Debugging.SelfLog.Enable(Console.Error);
        }


        /// <summary>
        /// The log Verbose
        /// </summary>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        public void LogVerbose(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Verbose(messageTemplate, propertyValues);
        }

        /// <summary>
        /// The log Verbose
        /// </summary>
        /// <param name="exception">
        /// The exception
        /// </param>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        public void LogVerbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Verbose(exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// The log Debug
        /// </summary>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        public void LogDebug(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Debug(messageTemplate, propertyValues);
        }

        /// <summary>
        /// The log Debug
        /// </summary>
        /// <param name="exception">
        /// The exception
        /// </param>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        public void LogDebug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Debug(exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// The log Information
        /// </summary>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        public void LogInformation(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Information(messageTemplate, propertyValues);
        }

        /// <summary>
        /// The log Information
        /// </summary>
        /// <param name="exception">
        /// The exception
        /// </param>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        public void LogInformation(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Information(exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// The log Warning
        /// </summary>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        public void LogWarning(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Warning(messageTemplate, propertyValues);
        }

        /// <summary>
        /// The log Warning
        /// </summary>
        /// <param name="exception">
        /// The exception
        /// </param>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        public void LogWarning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Warning(exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// The log Error
        /// </summary>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        public void LogError(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Error(messageTemplate, propertyValues);
        }

        /// <summary>
        /// The log Error
        /// </summary>
        /// <param name="exception">
        /// The exception
        /// </param>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        public void LogError(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Error(exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// The log Fatal
        /// </summary>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        public void LogFatal(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Fatal(messageTemplate, propertyValues);
        }

        /// <summary>
        /// The log Fatal
        /// </summary>
        /// <param name="exception">
        /// The exception
        /// </param>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        public void LogFatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Fatal(exception, messageTemplate, propertyValues);
        }
    }
}