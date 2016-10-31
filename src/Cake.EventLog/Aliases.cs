using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.EventLog.Exceptions;

namespace Cake.EventLog
{
    /// <summary>
    ///     Alias to work with the Windows Event Log
    /// </summary>
    [CakeAliasCategory("Event Log")]
    [CakeNamespaceImport("Cake.EventLog")]
    public static partial class EventLogAliases
    {
        /// <summary>
        ///     Writes the given message to the Event Log using the given settings.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <param name="message">The message to write to the log.</param>
        /// <param name="configure">Action to configure logging behaviour.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <see cref="ICakeContext" /> is null.</exception>
        [CakeMethodAlias]
        public static void WriteToEventLog(this ICakeContext ctx, string message,
            Action<EventLogEntrySettings> configure)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (ctx.IsInvalid()) NotSupported();
            var settings = new EventLogEntrySettings();
            configure?.Invoke(settings);
            var mgr = new EventLogManager(settings, ctx.Log);
            mgr.WriteToEventLog(message);
        }

        /// <summary>
        ///     Writes the given message to the Event Log using the default settings.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <param name="message">The message to write to the log.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <see cref="ICakeContext" /> is null.</exception>
        [CakeMethodAlias]
        public static void WriteToEventLog(this ICakeContext ctx, string message)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (ctx.IsInvalid()) NotSupported();
            var settings = new EventLogEntrySettings();
            var mgr = new EventLogManager(settings, ctx.Log);
            mgr.WriteToEventLog(message);
        }

        /// <summary>
        ///     Creates the named event log on the local machine.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <param name="logName">The name of the log to create.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <see cref="ICakeContext" /> is null.</exception>
        [CakeMethodAlias]
        public static void CreateEventLog(this ICakeContext ctx, string logName)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (ctx.IsInvalid()) NotSupported();
            var settings = new EventLogEntrySettings {LogName = logName};
            var mgr = new EventLogManager(settings, ctx.Log);
            mgr.CreateLog();
        }

        /// <summary>
        ///     Checks if the named event log exists on the local machine.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <param name="logName">The name of the log to check for.</param>
        /// <returns>Whether the log exists.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <see cref="ICakeContext" /> is null.</exception>
        [CakeMethodAlias]
        public static bool EventLogExists(this ICakeContext ctx, string logName)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (ctx.IsInvalid()) NotSupported();
            var settings = new EventLogEntrySettings {LogName = logName};
            var mgr = new EventLogManager(settings, ctx.Log);
            return mgr.LogExists();
        }

        /// <summary>
        ///     Ensures that the named event log exists on the local machine.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <param name="logName">The log name to check or create.</param>
        /// <returns>Whether the named log exists.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <see cref="ICakeContext" /> is null.</exception>
        public static bool EnsureLogExists(this ICakeContext ctx, string logName)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (ctx.IsInvalid()) NotSupported();
            var settings = new EventLogEntrySettings {LogName = logName};
            return EnsureLogExists(settings, ctx.Log);
        }

        /// <summary>
        ///     Ensures that the named event log exists.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <param name="configure">Settings for the log.</param>
        /// <returns>Whether the named log exists.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <see cref="ICakeContext" /> is null.</exception>
        [CakeMethodAlias]
        public static bool EnsureLogExists(this ICakeContext ctx, Action<EventLogSettings> configure)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (ctx.IsInvalid()) NotSupported();
            var settings = new EventLogEntrySettings();
            configure?.Invoke(settings);
            return EnsureLogExists(settings, ctx.Log);
        }

        /// <summary>
        ///     Checks if the named event source exists on the local machine.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <param name="sourceName">The event source to check for.</param>
        /// <returns>Whether the given event source exists.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <see cref="ICakeContext" /> is null.</exception>
        [CakeMethodAlias]
        public static bool EventSourceExists(this ICakeContext ctx, string sourceName)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (ctx.IsInvalid()) NotSupported();
            var settings = new EventLogEntrySettings {SourceName = sourceName};
            var mgr = new EventLogManager(settings, ctx.Log);
            var exists = mgr.SourceExists();
            return exists.HasValue && exists.Value;
        }

        /// <summary>
        ///     Ensures that the named event source exists on the local machine.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <param name="sourceName">The event source to check or create.</param>
        /// <returns>Whether the event source exists or was created.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <see cref="ICakeContext" /> is null.</exception>
        [CakeMethodAlias]
        public static bool EnsureSourceExists(this ICakeContext ctx, string sourceName)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (ctx.IsInvalid()) NotSupported();
            var settings = new EventLogEntrySettings
            {
                SourceName = sourceName
            };
            return EnsureSourceExists(settings, ctx.Log);
        }

        /// <summary>
        ///     Ensures that the event source exists in the given event log.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <param name="configure">Settings to use when managing the event log.</param>
        /// <returns>Whether the configured source exists.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <see cref="ICakeContext" /> is null.</exception>
        [CakeMethodAlias]
        public static bool EnsureSourceExists(this ICakeContext ctx, Action<EventLogSettings> configure)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (ctx.IsInvalid()) NotSupported();
            var settings = new EventLogEntrySettings();
            configure?.Invoke(settings);
            return EnsureSourceExists(settings, ctx.Log);
        }
    }
}