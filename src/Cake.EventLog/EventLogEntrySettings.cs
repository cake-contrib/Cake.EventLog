using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.EventLog
{
    public sealed class EventLogEntrySettings
    {
        internal EventLogEntrySettings()
        {
        }

        public string LogName { get; set; } = "Application";
        public string MachineName { get; set; } = System.Environment.MachineName;
        public string SourceName { get; set; } = "Cake Build";
        public EventLogEntryType EntryType { get; set; } = EventLogEntryType.Information;
    }
}
