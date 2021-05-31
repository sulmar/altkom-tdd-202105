using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public class LogEventArgs : EventArgs
    {
        public DateTime LogDate { get; set; }

        public LogEventArgs(DateTime logDate)
        {
            this.LogDate = logDate;
        }
    }

    public class Logger
    {
        public string LastMessage { get; set; }
        

        public event EventHandler<LogEventArgs> MessageLogged;

        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException();

            LastMessage = message;

            // Write the log to a storage
            // ...

            Save(message);

            MessageLogged?.Invoke(this, new LogEventArgs(DateTime.UtcNow));
        }

        private void Save(string message)
        {

        }
    }
}
