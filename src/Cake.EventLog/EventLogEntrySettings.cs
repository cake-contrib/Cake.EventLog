using System.Diagnostics;

namespace Cake.EventLog
{
    /// <summary>
    ///     Settings to control event log entries
    /// </summary>
    public sealed class EventLogEntrySettings : EventLogSettings
    {
        internal EventLogEntrySettings()
        {
        }

        public EventLogEntryType EntryType { get; set; } = EventLogEntryType.Information;
        public int? EventId { get; set; }
    }
}