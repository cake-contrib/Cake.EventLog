using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.Diagnostics;

namespace Cake.EventLog
{
    public partial class EventLogAliases
    {
        private static bool EnsureLogExists(EventLogEntrySettings settings, ICakeLog log)
        {
            var mgr = new EventLogManager(settings, log);
            if (mgr.LogExists()) return true;
            mgr.CreateLog();
            return mgr.LogExists();
        }

        private static bool EnsureSourceExists(EventLogEntrySettings settings, ICakeLog log)
        {
            var mgr = new EventLogManager(settings, log);
            var exists = mgr.SourceExists();
            if (exists.HasValue && exists.Value) return true;
            return mgr.EnsureSourceExists();
        }

        private static void NotSupported()
        {
            throw new NotSupportedException("This addin is only supported on Windows!");
        }
    }
}
