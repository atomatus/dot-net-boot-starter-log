﻿using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Com.Atomatus.Bootstarter
{
    /// <summary>
    /// Microsoft <see cref="ILogger"/> extensions.
    /// </summary>
    public static class ILoggerExtensions
    {
        #region Log base
        internal delegate void LogAction(string message, object[] args);
        internal delegate void LogError(Exception error, string message, object[] args);

        private static string LogTraceMessage(
            Type loggerType,
            Exception error,
            string message,
            string memberName,
            string sourceFile,
            int sourceLine)
        {
            string targetName = loggerType.IsGenericType ? 
                loggerType.GetGenericArguments()?.FirstOrDefault()?.Name : null;

            int errorLine = 0;
            string stackTrace = null;

            if(error != null)
            {
                var st = new StackTrace(error, true);
                var sf = st.GetFrame(st.FrameCount - 1);
                errorLine = sf?.GetFileLineNumber() ?? errorLine;
                stackTrace = error.StackTrace?.Replace(" at ", "\r\n\t\t└► at ", StringComparison.InvariantCultureIgnoreCase);
                memberName = string.IsNullOrEmpty(memberName) ? sf?.GetMethod()?.Name : memberName;
                sourceFile = string.IsNullOrEmpty(sourceFile) ? sf?.GetFileName() : sourceFile;
            }

            return new StringBuilder()
               .Append("Message: ").AppendLine(message ?? error?.Message ?? throw new ArgumentNullException(nameof(message)))
               .AppendLineIf(targetName != null, "\t► Target Name: ", targetName)
               .AppendLineIf(memberName != null, "\t► Member Name: ", memberName)
               .AppendLineIf(sourceFile != null, "\t► Source File: ", sourceFile)
               .AppendLineIf(sourceLine > 0, "\t► Source Line: ", sourceLine)
               .AppendLineIf(errorLine > 0, "\t► Error Line : ", errorLine)
               .AppendLineIf(stackTrace != null, "\t► Stack Trace: ", stackTrace)
               .ToString();
        }

        internal static void Log(
            LogAction action,
            LogError errAction,
            Type loggerType,
            Exception error,
            string message,
            object[] args,
            string memberName,
            string sourceFile,
            int sourceLine)
        {
            string trace = LogTraceMessage(
                       loggerType,
                       error,
                       message,
                       memberName,
                       sourceFile,
                       sourceLine);

            if (errAction != null && error != null)
            {
                errAction.Invoke(error, trace, args);
            }
            else
            {
                action.Invoke(trace, args);
            }
        }

        internal static void Log(
            LogAction action,
            Type loggerType,
            string message,
            object[] args,
            string memberName,
            string sourceFile,
            int sourceLine)
        {
            Log(action,
                null,
                loggerType,
                null,
                message,
                args,
                memberName,
                sourceFile,
                sourceLine);
        }
        #endregion

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
            Log(logger.LogCritical, logger.GetType(), message, args, memberName, filePath, lineNumber);
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
            Log(logger.LogCritical,
                logger.LogCritical,
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
            Log(logger.LogDebug, logger.GetType(), message, args, memberName, filePath, lineNumber);
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
            Log(logger.LogDebug,
                logger.LogDebug,
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
            Log(logger.LogError, logger.GetType(), message, args, memberName, filePath, lineNumber);
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
            Log(logger.LogError, 
                logger.LogError, 
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
            Log(logger.LogInformation, logger.GetType(), message, args, memberName, filePath, lineNumber);
        }
        #endregion

        #region Trace
        /// <summary>
        /// Formats and write a trace log containing 
        /// caller member name, file path and line number to
        /// identify where performered it.
        /// </summary>
        /// <param name="logger">current perform logging</param>
        /// <param name="message">message</param>
        /// <param name="memberName">member name</param>
        /// <param name="filePath">file path</param>
        /// <param name="lineNumber">line number</param>
        /// <param name="args">format string of the log message in message template format</param>
        public static void LogT([NotNull] this ILogger logger, string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            Log(logger.LogTrace, logger.GetType(), message, args, memberName, filePath, lineNumber);
        }

        /// <summary>
        /// Formats and write a trace log containing 
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
        public static void LogT([NotNull] this ILogger logger,
            Exception error,
            string message = null,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            Log(logger.LogTrace, 
                logger.LogTrace, 
                logger.GetType(), error, message, args, memberName, filePath, lineNumber);
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
            Log(logger.LogWarning, logger.GetType(), message, args, memberName, filePath, lineNumber);
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
            Log(logger.LogWarning,
                logger.LogWarning,
                logger.GetType(), error, message, args, memberName, filePath, lineNumber);
        }
        #endregion
    }
}
