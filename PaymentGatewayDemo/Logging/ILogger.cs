/// ----------------------------------------------------------------------
/// <summary>
/// Defines the ILogger interface.
/// </summary>
/// ----------------------------------------------------------------------
namespace PaymentGatewayDemo.Logging
{
    using System;

    /// <summary>
    /// ILogger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// The log Verbose
        /// </summary>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        void LogVerbose(string messageTemplate, params object[] propertyValues);

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
        void LogVerbose(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// The log Debug
        /// </summary>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        void LogDebug(string messageTemplate, params object[] propertyValues);

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
        void LogDebug(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// The log Information
        /// </summary>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        void LogInformation(string messageTemplate, params object[] propertyValues);

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
        void LogInformation(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// The log Warning
        /// </summary>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        void LogWarning(string messageTemplate, params object[] propertyValues);

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
        void LogWarning(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// The log Error
        /// </summary>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        void LogError(string messageTemplate, params object[] propertyValues);

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
        void LogError(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// The log Fatal
        /// </summary>
        /// <param name="messageTemplate">
        /// The message template
        /// </param>
        /// <param name="propertyValues">
        /// The property values
        /// </param>
        void LogFatal(string messageTemplate, params object[] propertyValues);

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
        void LogFatal(Exception exception, string messageTemplate, params object[] propertyValues);
    }
}