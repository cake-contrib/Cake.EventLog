using System;
using System.Diagnostics;
using System.Security;
using Cake.Core;
using Cake.Core.Diagnostics;

namespace Cake.EventLog
{
    internal class EventLogManager
    {
        private readonly ICakeLog _log;

        private EventLogManager(ICakeLog log)
        {
            _log = log;
        }

        internal EventLogManager(EventLogEntrySettings settings = null, ICakeLog log = null) : this(log)
        {
            Settings = settings ?? new EventLogEntrySettings();
        }

        private EventLogEntrySettings Settings { get; }

        private System.Diagnostics.EventLog GetLog()
        {
            return new System.Diagnostics.EventLog(Settings.LogName, Settings.MachineName, Settings.SourceName);
        }

        internal void WriteToEventLog(string message, int? eventId = null)
        {
            var log = GetLog();
            eventId = eventId ?? Settings.EventId;
            if (!EnsureSourceExists()) throw new CakeException("Event source does not exist and could not be created!");
            if (eventId.HasValue)
            {
                log.WriteEntry(message, Settings.EntryType, eventId.Value);
            }
            else
            {
                log.WriteEntry(message, Settings.EntryType);
            }
        }

        internal bool EnsureSourceExists()
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
                _log?.Warning("Could not determine if the source exists. Attempting creation.");
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

        internal bool LogExists()
        {
            return System.Diagnostics.EventLog.Exists(Settings.LogName, Settings.MachineName);
        }
    }
}