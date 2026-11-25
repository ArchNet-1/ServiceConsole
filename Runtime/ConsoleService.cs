// ArchNet · Console Service
// A second console for your own messages, sorted by priority (feature, important, loop…)
// and shown in its own window: ArchNet > Services > Console. Call ConsoleService.Log /
// Warning / Error from anywhere; each entry keeps the call stack that produced it.
// Docs: https://archnet-plateform.fr/#ServiceConsole

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace ArchNet.Service
{
    public static class ConsoleService
    {
        static readonly AdvancedSettings _advancedSettings = new AdvancedSettings();
        static readonly List<Log> _all = new List<Log>();
        static readonly List<Log> _logs = new List<Log>();
        static readonly List<Log> _warnings = new List<Log>();
        static readonly List<Log> _errors = new List<Log>();
        static Log _selectedLog;
        static int _nextId = 1;
        static bool _showLogs, _showWarnings, _showErrors;

        /// <summary>Raised for every new entry (e.g. to show it on screen).</summary>
        public static event Action<Log> Logged;

        public static IReadOnlyList<Log> Logs => _logs;
        public static IReadOnlyList<Log> Warnings => _warnings;
        public static IReadOnlyList<Log> Errors => _errors;

        public static void Log(string title, eLogPriority priority = eLogPriority.NONE, string message = "") => Add(eLogType.LOG, title, priority, message);
        public static void Warning(string title, eLogPriority priority = eLogPriority.NONE, string message = "") => Add(eLogType.WARNING, title, priority, message);
        public static void Error(string title, eLogPriority priority = eLogPriority.NONE, string message = "") => Add(eLogType.ERROR, title, priority, message);
        public static void Exception(string title, eLogPriority priority = eLogPriority.NONE, string message = "") => Add(eLogType.EXCEPTION, title, priority, message);
        public static void Assert(string title, eLogPriority priority = eLogPriority.NONE, string message = "") => Add(eLogType.ASSERT, title, priority, message);

        /// <summary>Removes every entry.</summary>
        public static void Reset()
        {
            _all.Clear();
            _logs.Clear();
            _warnings.Clear();
            _errors.Clear();
            _selectedLog = null;
            _nextId = 1;
        }

        /// <summary>Every entry, oldest first.</summary>
        public static List<Log> GetAll() => _all;

        /// <summary>Entries whose title contains the text (case ignored).</summary>
        public static List<Log> Find(string text) =>
            string.IsNullOrEmpty(text) ? new List<Log>(_all) : _all.FindAll(l => (l.GetTitle() ?? "").IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);

        public static List<Log> GetLogFromPriority(eLogPriority priority) => _all.FindAll(l => l.GetLogPriority() == priority);

        public static AdvancedSettings GetAdvancedSettings() => _advancedSettings;

        public static Log GetSelectedLog() => _selectedLog;
        public static void SetSelectedLog(Log log) { if (log != null) _selectedLog = log; }

        // On-screen panels (ConsoleServiceRuntime).
        public static bool GetLogEvent() => _showLogs;
        public static bool GetWarningEvent() => _showWarnings;
        public static bool GetErrorEvent() => _showErrors;
        public static void SetLogEvent(bool show) => _showLogs = show;
        public static void SetWarningEvent(bool show) => _showWarnings = show;
        public static void SetErrorEvent(bool show) => _showErrors = show;

        /// <summary>Unity's log type as a Console Service type.</summary>
        public static eLogType ConvertLogType(LogType type) => type switch
        {
            LogType.Log => eLogType.LOG,
            LogType.Warning => eLogType.WARNING,
            LogType.Assert => eLogType.ASSERT,
            LogType.Error => eLogType.ERROR,
            LogType.Exception => eLogType.EXCEPTION,
            _ => eLogType.NONE,
        };

        static void Add(eLogType type, string title, eLogPriority priority, string message)
        {
            var log = new Log();
            log.SetId(_nextId++);
            log.SetTitle(title);
            log.SetMessage(message);
            log.SetType(type);
            log.SetPriority(priority);
            log.SetStack(ReadStack(new StackTrace(2, true))); // skip Add and the public method

            _all.Add(log);
            ListFor(type).Add(log);
            Logged?.Invoke(log);
        }

        static List<Log> ListFor(eLogType type) => type switch
        {
            eLogType.ERROR or eLogType.EXCEPTION => _errors,
            eLogType.WARNING or eLogType.ASSERT => _warnings,
            _ => _logs,
        };

        static string ReadStack(StackTrace stack)
        {
            var text = new StringBuilder();
            for (int i = 0; i < stack.FrameCount; i++)
            {
                StackFrame frame = stack.GetFrame(i);
                if (frame?.GetMethod() == null) continue;
                text.Append("\n_______________________________________________________________");
                text.Append("\nFile : ").Append(frame.GetFileName());
                text.Append("\nMethod : ").Append(frame.GetMethod().Name).Append("()");
                text.Append("\nLine : ").Append(frame.GetFileLineNumber());
            }
            return text.ToString();
        }
    }
}
