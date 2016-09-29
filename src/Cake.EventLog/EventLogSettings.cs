using System;

namespace Cake.EventLog
{
    /// <summary>
    ///     Settings to control the Windows Event Log
    /// </summary>
    public class EventLogSettings
    {
        internal EventLogSettings()
        {
        }

        public string LogName { get; set; } = "Application";
        public string MachineName { get; set; } = Environment.MachineName;
        public string SourceName { get; set; } = "Cake Build";
    }
}