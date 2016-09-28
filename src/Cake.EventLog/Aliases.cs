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
    public static class EventLogAliases
    {
        [CakeMethodAlias]
        public static void WriteToEventLog(this ICakeContext ctx, string message, Action<EventLogEntrySettings> configure = null)
        {
            var settings = new EventLogEntrySettings();
            configure?.Invoke(settings);
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            var mgr = new EventLogManager(settings, ctx.Log);
            mgr.WriteToEventLog(message);
        }

        [CakeMethodAlias]
        public static void CreateEventLog(this ICakeContext ctx, string logName)
        {
            var settings = new EventLogEntrySettings {LogName = logName};
            var mgr = new EventLogManager(settings, ctx.Log);
            mgr.CreateLog();
        }

        [CakeMethodAlias]
        public static bool EventLogExists(this ICakeContext ctx, string logName)
        {
            var settings = new EventLogEntrySettings() { LogName = logName };
            var mgr = new EventLogManager(settings, ctx.Log);
            return mgr.LogExists();
        }

        [CakeMethodAlias]
        public static bool EnsureSourceExists(this ICakeContext ctx, Action<EventLogEntrySettings> configure = null)
        {
            var settings = new EventLogEntrySettings();
            configure?.Invoke(settings);
            var mgr = new EventLogManager(settings);
            var exists = mgr.SourceExists();
            if (exists.HasValue && exists.Value) return exists.Value;
            return mgr.EnsureSourceExists();
        }
    }
}