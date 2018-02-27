﻿using System;
using System.IO;

namespace WebService.Services.Logging
{
    /// <inheritdoc cref="ILogger"/>
    /// <summary>
    /// FileLogger is a class that implements the ILogger Interface.
    /// <para>
    /// It logs messages to a file. The log messages are built with an <see cref="ILogBuilder"/> instance.
    /// If the logbuilder is injected in the constructor, an instance of the <see cref="LogBuilder"/> class is used.
    /// </para>
    /// </summary>
    public class FileLogger : ILogger
    {
        #region FIELDS

        /// <summary>
        /// _logBuilder is the instance to create the messages to log.
        /// </summary>
        private readonly ILogBuilder _logBuilder;

        #endregion FIELDS


        #region CONSTRUCTOR

        /// <summary>
        /// FileLogger is the constructor to create an instance of the <see cref="FileLogger"/> class.
        /// </summary>
        /// <param name="logBuilder">is the instance to create the messages to log</param>
        public FileLogger(ILogBuilder logBuilder)
        {
            // set the field with the given value
            _logBuilder = logBuilder;
        }

        /// <summary>
        /// FileLogger is the constructor to create an instance of the <see cref="FileLogger"/> class.
        /// <para/>
        /// Becaus there is no <see cref="ILogBuilder"/> injected, the default is used (<see cref="LogBuilder"/>).
        /// </summary>
        public FileLogger()
        {
            // set the field with a new instance of the LogBuilder class
            _logBuilder = new LogBuilder();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        /// <summary>
        /// FilePath is the relative location where the logs are stored.
        /// The name is path is generated by using the current date and adding the .log extension
        /// </summary>
        private static string FilePath
            // return the formatted current date
            => $"{DateTime.Now:yyyy-MMMM-dd}.log";

        #endregion PROPERTIES


        #region METHODS

        /// <inheritdoc cref="ILogger.Log{T}(T,ELogLevel,Exception)"/>
        /// <summary>
        /// Log&lt;T&gt; logs the message of an exception to the file.
        /// <para/>
        /// If the logLevel is <see cref="ELogLevel.Debug"/>, the messages are only shown in the debug build of the app.
        /// </summary>
        /// <typeparam name="T">is the type of the sender that wants to log the exception</typeparam>
        /// <param name="sender">is that wants to log the exception</param>
        /// <param name="logLevel">is the importance of the log</param>
        /// <param name="exception">is the exception to log the message of</param>
        public void Log<T>(T sender, ELogLevel logLevel, Exception exception)
        {
            // if the app is in debug mode and the loglevel is debug, do nothing
#if DEBUG
            if (logLevel == ELogLevel.Debug)
                return;
#endif
            try
            {
                // if the filePath doesn't exists, create it
                if (!File.Exists(FilePath))
                    File.Create(FilePath);

                // create the log message using the message of the exception
                var log = _logBuilder.BuildLogEntry(typeof(T).Name, logLevel, exception.Message);

                // add the log-message to the file
                File.AppendAllLines(FilePath, new[] {log});
            }
            catch (IOException)
            {
                // IGNORED
            }
        }

        /// <inheritdoc cref="ILogger.Log{T}(T,ELogLevel,string)"/>
        /// <summary>
        /// Log&lt;T&gt; logs a message to the file.
        /// <para/>
        /// If the logLevel is <see cref="ELogLevel.Debug"/>, the messages are only shown in the debug build of the app.
        /// </summary>
        /// <typeparam name="T">is the type of the sender that wants to log the message</typeparam>
        /// <param name="sender">is that wants to log the exception</param>
        /// <param name="logLevel">is the importance of the log</param>
        /// <param name="message">is the message to log</param>
        public void Log<T>(T sender, ELogLevel logLevel, string message)
        {
            // if the app is in debug mode and the loglevel is debug, do nothing
#if DEBUG
            if (logLevel == ELogLevel.Debug)
                return;
#endif
            try
            {
                // if the filePath doesn't exists, create it
                if (!File.Exists(FilePath))
                    File.Create(FilePath);

                // create the log message
                var log = _logBuilder.BuildLogEntry(typeof(T).Name, logLevel, message);

                // add the log-message to the file
                File.AppendAllLines(FilePath, new[] {log});
            }
            catch (IOException)
            {
                // IGNORED
            }
        }

        #endregion METHODS
    }
}