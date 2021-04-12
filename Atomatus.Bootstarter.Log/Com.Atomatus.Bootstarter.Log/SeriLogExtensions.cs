using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Com.Atomatus.Bootstarter
{
    public static class SeriLogExtensions
    {
        #region Critical
        public static void LogC([NotNull] this ILogger logger, string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Fatal, logger.GetType(), message, args, memberName, filePath, lineNumber);
        }

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
        public static void LogD([NotNull] this ILogger logger, string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Debug, logger.GetType(), message, args, memberName, filePath, lineNumber);
        }

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
        public static void LogE([NotNull] this ILogger logger, string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Error, logger.GetType(), message, args, memberName, filePath, lineNumber);
        }

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
        public static void LogW([NotNull] this ILogger logger, string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            ILoggerExtensions.Log(logger.Warning, logger.GetType(), message, args, memberName, filePath, lineNumber);
        }

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
