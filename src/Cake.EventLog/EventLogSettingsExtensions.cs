namespace Cake.EventLog
{
    /// <summary>
    ///     Extension methods for working with <see cref="EventLogSettings" />
    /// </summary>
    public static class EventLogSettingsExtensions
    {
        /// <summary>
        ///     Logs to the named event log
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="logName">The named log to write to.</param>
        /// <returns>The updated settings.</returns>
        public static EventLogSettings LogTo(this EventLogSettings settings, string logName)
        {
            settings.LogName = logName;
            return settings;
        }

        /// <summary>
        ///     Manage logs for the named machine
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="machineName">The machine name to manage logs for.</param>
        /// <returns>The updated settings.</returns>
        public static EventLogSettings ForMachine(this EventLogSettings settings, string machineName)
        {
            settings.MachineName = machineName;
            return settings;
        }

        /// <summary>
        ///     Logs using the specified source name
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="sourceName">The source name to use for events.</param>
        /// <returns>The updated settings.</returns>
        public static EventLogSettings WithSourceName(this EventLogSettings settings, string sourceName)
        {
            settings.SourceName = sourceName;
            return settings;
        }
    }
}