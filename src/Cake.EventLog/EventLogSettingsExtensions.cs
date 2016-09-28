using System.Diagnostics;
using Cake.Core.Diagnostics;

namespace Cake.EventLog
{
    /// <summary>
    ///     Extension methods for working with <see cref="EventLogEntrySettings" />
    /// </summary>
    public static class EventLogSettingsExtensions
    {
        /// <summary>
        ///     Sets the entry level (type) to the given value
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="type">The desired entry type.</param>
        /// <returns>The settings.</returns>
        public static EventLogEntrySettings WithLogLevel(this EventLogEntrySettings settings, EventLogEntryType type)
        {
            settings.EntryType = type;
            return settings;
        }

        /// <summary>
        ///     Sets the entry type to mattch the given log level
        /// </summary>
        /// <remarks>
        ///     This performs a mapping from the relevant Cake <see cref="LogLevel" /> to a corresponding
        ///     <see cref="EventLogEntryType" /> and some levels may be mapped to the same type.
        /// </remarks>
        /// <param name="settings">The settings.</param>
        /// <param name="level">The desired log level.</param>
        /// <returns>The settings.</returns>
        public static EventLogEntrySettings WithLogLevel(this EventLogEntrySettings settings, LogLevel level)
        {
            settings.EntryType = level.ToEntryType();
            return settings;
        }
    }
}