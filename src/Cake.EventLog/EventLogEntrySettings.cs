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

        /// <summary>
        ///     Sets the entry type to be written to the log. Defaults to Information.
        /// </summary>
        public EventLogEntryType EntryType { get; set; } = EventLogEntryType.Information;

        /// <summary>
        ///     Optional event ID to use when writing the entry to the log.
        /// </summary>
        public int? EventId { get; set; }
    }
}