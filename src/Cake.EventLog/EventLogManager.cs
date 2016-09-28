using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Diagnostics;

namespace Cake.EventLog
{
    internal class EventLogManager
    {
        private readonly ICakeLog _log;
        private EventLogEntrySettings Settings { get; set; }

        private EventLogManager(ICakeLog log)
        {
            _log = log;
        }

        internal EventLogManager(EventLogEntrySettings settings = null, ICakeLog log = null) : this(log)
        {
            Settings = settings ?? new EventLogEntrySettings();
        }

        private System.Diagnostics.EventLog GetLog()
        {
            return new System.Diagnostics.EventLog(Settings.LogName, Settings.MachineName, Settings.SourceName);
        }

        internal void WriteToEventLog(string message, int? eventId = null)
        {
            var log = GetLog();
            if (!EnsureSourceExists()) throw new CakeException("Event source does not exist and could not be created!");
            if (eventId.HasValue)
            {
                log.WriteEntry(message, Settings.EntryType);
            }
            else
            {
                log.WriteEntry(message, Settings.EntryType);
            }
        }

        private bool EnsureSourceExists()
        {
            var data = new EventSourceCreationData(Settings.SourceName, Settings.LogName)
            {
                MachineName = Settings.MachineName
            };
            var exists = false;
            try
            {
                exists = System.Diagnostics.EventLog.Exists(Settings.SourceName);
                if (!exists)
                {
                    _log?.Information("Source does not exist. Creating...");
                    System.Diagnostics.EventLog.CreateEventSource(data);
                    exists = System.Diagnostics.EventLog.SourceExists(Settings.SourceName);
                }
            }
            catch (SecurityException)
            {
                // we couldn't determine if the source exists
                LogPrivilegeWarning();
                _log?.Information("Could not determine if the source exists. Attempting creation.");
                try
                {
                    System.Diagnostics.EventLog.CreateEventSource(data);
                }
                catch (ArgumentException)
                {
                    // couldn't test if exists but creation failed
                    _log?.Information("Creating event source failed! The source apepars to already exist.");
                    exists = true;
                }
            }
            catch (Exception)
            {
                exists = false;
            }
            return exists;
        }

        internal void CreateLog()
        {
            var log = GetLog();
            if (!EnsureSourceExists()) throw new CakeException("Event source does not exist and could not be created!");
            if (!System.Diagnostics.EventLog.Exists(Settings.LogName, Settings.MachineName))
            {
                WriteToEventLog($"Creating event log {Settings.LogName}", 0);
            }
        }

        internal bool? SourceExists()
        {
            try
            {
                return System.Diagnostics.EventLog.SourceExists(Settings.SourceName, Settings.MachineName);
            }
            catch (SecurityException)
            {
                LogPrivilegeWarning();
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void LogPrivilegeWarning()
        {
            _log?.Warning("It appears the script is not running with privileges");
        }
    }
}