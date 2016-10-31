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

        /// <summary>
        ///     Name of the event log to manage
        /// </summary>
        /// <remarks>Defaults to "Application"</remarks>
        public string LogName { get; set; } = "Application";

        /// <summary>
        ///     Machine name to manage logs for.
        /// </summary>
        /// <remarks>Defaults to Environment.MachineName</remarks>
        public string MachineName { get; set; } = Environment.MachineName;

        /// <summary>
        ///     Event source to use when logging
        /// </summary>
        /// <remarks>Defaults to "Cake Build"</remarks>
        public string SourceName { get; set; } = "Cake Build";
    }
}