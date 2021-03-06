﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Logging;

namespace SelfEducationProjectAPI.Logger
{
    public class FileLogger : ILogger
    {
        private string filePath;
        private static object _lock = new object();

        public FileLogger(string path)
        {
            filePath = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                if (exception != null)
                {
                    string message = $"{DateTime.Now}: {exception.Message}" + Environment.NewLine + exception.StackTrace + Environment.NewLine;
                    lock (_lock)
                    {
                        File.AppendAllText(filePath, message);
                    }
                }
            }
        }
    }
}
