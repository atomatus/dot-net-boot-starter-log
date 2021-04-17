using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Com.Atomatus.Bootstarter
{
    /// <summary>
    /// Serilog <see cref="ILogger"/> extensions.
    /// </summary>
    public static class SeriLogExtensions
    {
        #region Critical
        /// <summary>
        /// Formats and write a critical log containing 
        /// caller member name, file path and line number to
        /// identify where performered it.
        /// </summary>
        /// <param name="logger">current perform logging</param>
        /// <param name="message">message</param>
        /// <param name="memberName">member name</param>
        /// <param name="filePath">file path</param>
        /// <param name="lineNumber">line number</param>
        /// <param name="args">format string of the log message in message template format</param>
        public static void LogC([NotNull] this ILogger logger, string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Fatal, logger.GetType(), message, args, memberName, filePath, lineNumber);
        }

        /// <summary>
        /// Formats and write a critical log containing 
        /// caller member name, file path and line number to
        /// identify where performered it.
        /// </summary>
        /// <param name="logger">current perform logging</param>
        /// <param name="error">exception error</param>
        /// <param name="message">message</param>
        /// <param name="memberName">member name</param>
        /// <param name="filePath">file path</param>
        /// <param name="lineNumber">line number</param>
        /// <param name="args">format string of the log message in message template format</param>
        public static void LogC([NotNull] this ILogger logger,
            Exception error,
            string message = null,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Fatal,
                logger.Fatal,
                logger.GetType(), error, message, args, memberName, filePath, lineNumber);
        }
        #endregion

        #region Debug
        /// <summary>
        /// Formats and write a debug log containing 
        /// caller member name, file path and line number to
        /// identify where performered it.
        /// </summary>
        /// <param name="logger">current perform logging</param>
        /// <param name="message">message</param>
        /// <param name="memberName">member name</param>
        /// <param name="filePath">file path</param>
        /// <param name="lineNumber">line number</param>
        /// <param name="args">format string of the log message in message template format</param>
        public static void LogD([NotNull] this ILogger logger, string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Debug, logger.GetType(), message, args, memberName, filePath, lineNumber);
        }

        /// <summary>
        /// Formats and write a debug log containing 
        /// caller member name, file path and line number to
        /// identify where performered it.
        /// </summary>
        /// <param name="logger">current perform logging</param>
        /// <param name="error">exception error</param>
        /// <param name="message">message</param>
        /// <param name="memberName">member name</param>
        /// <param name="filePath">file path</param>
        /// <param name="lineNumber">line number</param>
        /// <param name="args">format string of the log message in message template format</param>
        public static void LogD([NotNull] this ILogger logger,
            Exception error,
            string message = null,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Debug,
                logger.Debug,
                logger.GetType(), error, message, args, memberName, filePath, lineNumber);
        }
        #endregion

        #region Error
        /// <summary>
        /// Formats and write an error log containing 
        /// caller member name, file path and line number to
        /// identify where performered it.
        /// </summary>
        /// <param name="logger">current perform logging</param>
        /// <param name="message">message</param>
        /// <param name="memberName">member name</param>
        /// <param name="filePath">file path</param>
        /// <param name="lineNumber">line number</param>
        /// <param name="args">format string of the log message in message template format</param>
        public static void LogE([NotNull] this ILogger logger, string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Error, logger.GetType(), message, args, memberName, filePath, lineNumber);
        }

        /// <summary>
        /// Formats and write an error log containing 
        /// caller member name, file path and line number to
        /// identify where performered it.
        /// </summary>
        /// <param name="logger">current perform logging</param>
        /// <param name="error">exception error</param>
        /// <param name="message">message</param>
        /// <param name="memberName">member name</param>
        /// <param name="filePath">file path</param>
        /// <param name="lineNumber">line number</param>
        /// <param name="args">format string of the log message in message template format</param>
        public static void LogE([NotNull] this ILogger logger,
            Exception error,
            string message = null,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Error,
                logger.Error,
                logger.GetType(), error, message, args, memberName, filePath, lineNumber);
        }
        #endregion

        #region Info
        /// <summary>
        /// Formats and write an information log containing 
        /// caller member name, file path and line number to
        /// identify where performered it.
        /// </summary>
        /// <param name="logger">current perform logging</param>
        /// <param name="message">message</param>
        /// <param name="memberName">member name</param>
        /// <param name="filePath">file path</param>
        /// <param name="lineNumber">line number</param>
        /// <param name="args">format string of the log message in message template format</param>
        public static void LogI([NotNull] this ILogger logger, string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Information, logger.GetType(), message, args, memberName, filePath, lineNumber);
        }
        #endregion

        #region Warn
        /// <summary>
        /// Formats and write a warning log containing 
        /// caller member name, file path and line number to
        /// identify where performered it.
        /// </summary>
        /// <param name="logger">current perform logging</param>
        /// <param name="message">message</param>
        /// <param name="memberName">member name</param>
        /// <param name="filePath">file path</param>
        /// <param name="lineNumber">line number</param>
        /// <param name="args">format string of the log message in message template format</param>
        public static void LogW([NotNull] this ILogger logger, string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Warning, logger.GetType(), message, args, memberName, filePath, lineNumber);
        }

        /// <summary>
        /// Formats and write a warning log containing 
        /// caller member name, file path and line number to
        /// identify where performered it.
        /// </summary>
        /// <param name="logger">current perform logging</param>
        /// <param name="error">exception error</param>
        /// <param name="message">message</param>
        /// <param name="memberName">member name</param>
        /// <param name="filePath">file path</param>
        /// <param name="lineNumber">line number</param>
        /// <param name="args">format string of the log message in message template format</param>
        public static void LogW([NotNull] this ILogger logger,
            Exception error,
            string message = null,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Warning,
                logger.Warning,
                logger.GetType(), error, message, args, memberName, filePath, lineNumber);
        }
        #endregion
    }
}
