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
        public static void WriteToEventLog(this ICakeContext ctx, string logName, string message)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            var mgr = new EventLogManager();
        }
    }
}