using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;

namespace Cake.EventLog
{
    /// <summary>
    /// Alias to work with the Windows Event Log
    /// </summary>
    [CakeAliasCategory("Event Log")]
    [CakeNamespaceImport("Cake.EventLog")]
    public static class EventLogAliases
    {
        [CakeMethodAlias]
        public static void WriteToEventLog(this ICakeContext ctx, string message, Action<EventLogEntrySettings> configure = null)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            var settings = new EventLogEntrySettings();
            configure?.Invoke(settings);
            var mgr = new EventLogManager(settings, ctx.Log);
            mgr.WriteToEventLog(message);
        }

        [CakeMethodAlias]
        public static void CreateEventLog(this ICakeContext ctx, string logName)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            var settings = new EventLogEntrySettings {LogName = logName};
            var mgr = new EventLogManager(settings, ctx.Log);
            mgr.CreateLog();
        }

        [CakeMethodAlias]
        public static bool EventLogExists(this ICakeContext ctx, string logName)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            var settings = new EventLogEntrySettings() { LogName = logName };
            var mgr = new EventLogManager(settings, ctx.Log);
            return mgr.LogExists();
        }

        [CakeMethodAlias]
        public static bool EnsureLogExists(this ICakeContext ctx, string logName, Action<EventLogSettings> configure = null)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            var settings = new EventLogEntrySettings();
            configure?.Invoke(settings);
            settings.LogName = logName;
            var mgr = new EventLogManager(settings);
            if (mgr.LogExists()) return true;
            mgr.CreateLog();
            return mgr.LogExists();
        }

        [CakeMethodAlias]
        public static bool EventSourceExists(this ICakeContext ctx, string sourceName, Action<EventLogSettings> configure = null)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            var settings = new EventLogEntrySettings() {SourceName = sourceName};
            configure?.Invoke(settings);
            var mgr = new EventLogManager(settings);
            var exists = mgr.SourceExists();
            return exists.HasValue && exists.Value;
        }

        [CakeMethodAlias]
        public static bool EnsureSourceExists(this ICakeContext ctx, Action<EventLogSettings> configure = null)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            var settings = new EventLogEntrySettings();
            configure?.Invoke(settings);
            var mgr = new EventLogManager(settings);
            var exists = mgr.SourceExists();
            if (exists.HasValue && exists.Value) return true;
            return mgr.EnsureSourceExists();
        }
    }
}