using ArchNet.Service.Console.Enum;
using ArchNet.Service.Console.Model;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace ArchNet.Service.Console
{
    /// <summary>
    /// [SERVICE] - [ARCH NET] - [CONSOLE] : Service Custom Console
    /// @author : LOUIS PAKEL
    /// </summary>
    public static class ConsoleService
    {
        #region Private Fields

        private static AdvancedSettings _advancedSettings = new AdvancedSettings();

        // Log message are active
        private static bool _log = false;
        // Error message are active
        private static bool _error = false;
        // Warning message are active
        private static bool _warning = false;

        // all logs
        private static List<Log> _allLogs = new List<Log>();

        // All private list of log types
        private static List<Log> _logs = new List<Log>();
        private static List<Log> _warnings = new List<Log>();
        private static List<Log> _errors = new List<Log>();

        // Selected log in the ConsoleServiceEditor
        private static Log _selectedLog;

        #endregion
        /// <summary>
        /// Description : Reset all logs
        /// </summary>
        public static void Reset()
        {
            _allLogs = new List<Log>();
            _logs = new List<Log>();
            _warnings = new List<Log>();
            _errors = new List<Log>();
        }

    public static  void Test()
        {
            ConsoleService.Log("This is a feature log message", eLogPriority.FEATURE);
            ConsoleService.Error("This is a feature error message", eLogPriority.FEATURE);
            ConsoleService.Warning("This is a feature warning message", eLogPriority.FEATURE);


            ConsoleService.Log("This is a important log message", eLogPriority.IMPORTANT);
            ConsoleService.Error("This is a important log message", eLogPriority.IMPORTANT);
            ConsoleService.Warning("This is a important warning message", eLogPriority.IMPORTANT);

            ConsoleService.Log("This is a methods log message", eLogPriority.METHODS);
            ConsoleService.Error("This is a methods error message", eLogPriority.METHODS);
            ConsoleService.Warning("This is a methods warning message", eLogPriority.METHODS);

            ConsoleService.Log("This is a loop log message", eLogPriority.LOOP);
            ConsoleService.Error("This is a loop error message", eLogPriority.LOOP);
            ConsoleService.Warning("This is a loop warning message", eLogPriority.LOOP);

            ConsoleService.Log("This is a custom log message", eLogPriority.CUSTOM);
            ConsoleService.Error("This is a custom error message", eLogPriority.CUSTOM);
            ConsoleService.Warning("This is a custom warning message", eLogPriority.CUSTOM);
        }

        #region Micro Services

        /// <summary>
        /// Description : Get Stack of the log
        /// </summary>
        /// <param name="pStack"></param>
        /// <returns></returns>
        private static string GetStack(StackTrace pStack)
        {
            string lResult = "";

            for (int i = 1; i < pStack.FrameCount; i++)
            {
                lResult += "\n_______________________________________________________________";
                lResult += "\nFile : " + pStack.GetFrame(i).GetFileName();
                lResult += "\nMethod : " + pStack.GetFrame(i).GetMethod().Name + "()";
                lResult += "\nLine : " + pStack.GetFrame(i).GetFileLineNumber();
            }

            return lResult;
        }

        /// <summary>
        /// Description : Log Message in Custom Window ( Console Service Window )
        /// </summary>
        public static void Log(string pTitle, eLogPriority pPriority = eLogPriority.NONE, string pMessage = "")
        {
            StackTrace stackTrace = new StackTrace(true);

            // Declare new log
            Log _newLog = new Log();
            _newLog.SetId(_logs.Count + 1);
            _newLog.SetMessage(pMessage);
            _newLog.SetTitle(pTitle);
            _newLog.SetType(eLogType.LOG);
            _newLog.SetPriority(pPriority);
            _newLog.SetStack(GetStack(stackTrace));

            // Add log in list
            _logs.Add(_newLog);
            _allLogs.Add(_newLog);
        }

        /// <summary>
        /// Description : Error Message in Custom Window ( Console Service Window )
        /// </summary>
        public static void Error(string pTitle, eLogPriority pPriority = eLogPriority.NONE, string pMessage = "")
        {
            StackTrace stackTrace = new StackTrace(true);

            // Declare new log
            Log _newLog = new Log();
            _newLog.SetId(_errors.Count + 1);
            _newLog.SetMessage(pMessage);
            _newLog.SetTitle(pTitle);
            _newLog.SetType(eLogType.ERROR);
            _newLog.SetPriority(pPriority);
            _newLog.SetStack(GetStack(stackTrace));

            // Add log in list
            _errors.Add(_newLog);
            _allLogs.Add(_newLog);
        }

        /// <summary>
        /// Description : Warning Message in Custom Window ( Console Service Window )
        /// </summary>
        public static void Warning(string pTitle, eLogPriority pPriority = eLogPriority.NONE, string pMessage = "")
        {
            StackTrace stackTrace = new StackTrace(true);

            // Declare new log
            Log _newLog = new Log();
            _newLog.SetId(_warnings.Count + 1);
            _newLog.SetMessage(pMessage);
            _newLog.SetTitle(pTitle);
            _newLog.SetType(eLogType.WARNING);
            _newLog.SetPriority(pPriority);
            _newLog.SetStack(GetStack(stackTrace));

            // Add log in list
            _warnings.Add(_newLog);
            _allLogs.Add(_newLog);
        }


        /// <summary>
        /// Description : Exception Message in Custom Window ( Console Service Window )
        /// </summary>
        public static void Exception(string pTitle, eLogPriority pPriority = eLogPriority.NONE, string pMessage = "")
        {
            StackTrace stackTrace = new StackTrace(true);

            // Declare new log
            Log _newLog = new Log();
            _newLog.SetId(_warnings.Count + 1);
            _newLog.SetMessage(pMessage);
            _newLog.SetTitle(pTitle);
            _newLog.SetType(eLogType.EXCEPTION);
            _newLog.SetPriority(pPriority);
            _newLog.SetStack(GetStack(stackTrace));

            // Add log in list
            _warnings.Add(_newLog);
            _allLogs.Add(_newLog);
        }

        /// <summary>
        /// Description : Assert Message in Custom Window ( Console Service Window )
        /// </summary>
        public static void Assert(string pTitle, eLogPriority pPriority = eLogPriority.NONE, string pMessage = "")
        {
            StackTrace stackTrace = new StackTrace(true);

            // Declare new log
            Log _newLog = new Log();
            _newLog.SetId(_warnings.Count + 1);
            _newLog.SetMessage(pMessage);
            _newLog.SetTitle(pTitle);
            _newLog.SetType(eLogType.ASSERT);
            _newLog.SetPriority(pPriority);
            _newLog.SetStack(GetStack(stackTrace));

            // Add log in list
            _warnings.Add(_newLog);
            _allLogs.Add(_newLog);
        }

        #endregion


        #region Getter / Setter

        public static AdvancedSettings GetAdvancedSettings()
        {
            return _advancedSettings;
        }

        /// <summary>
        /// Description : Get log from priority
        /// </summary>
        /// <param name="pPriority"></param>
        /// <returns></returns>
        public static List<Log> GetLogs(string pfilter)
        {
            List<Log> lResult = new List<Log>();

            foreach (Log lLog in _allLogs)
            {
                if (lLog.GetTitle().Contains(pfilter))
                {
                    lResult.Add(lLog);
                }
            }

            return lResult;
        }

        /// <summary>
        /// Description : Get log from priority
        /// </summary>
        /// <param name="pPriority"></param>
        /// <returns></returns>
        public static List<Log> GetLogFromPriority(eLogPriority pPriority)
        {
            List<Log> lResult = new List<Log>();

            foreach(Log lLog in _allLogs)
            {
                if(lLog.GetLogPriority() == pPriority)
                {
                    lResult.Add(lLog);
                }
            }

            return lResult;
        }

        public static Log GetSelectedLog()
        {
            return _selectedLog;
        }

        public static void SetSelectedLog(Log pSelectedLog)
        {
            if(pSelectedLog != null)
            {
                _selectedLog = pSelectedLog;
            }
        }

        public static void SetLogEvent(bool pLog)
        {
            _log = pLog;
        }

        public static void SetErrorEvent(bool pError)
        {
            _error = pError;
        }

        public static void SetWarningEvent(bool pWarning)
        {
            _warning = pWarning;
        }

        public static bool GetWarningEvent()
        {
            return _warning;
        }

        public static bool GetErrorEvent()
        {
            return _error;
        }

        public static bool GetLogEvent()
        {
            return _log;
        }


        public static List<Log> GetLogs()
        {
            return _logs;
        }

        public static List<Log> GetErrors()
        {
            return _errors;
        }

        public static List<Log> GetWarnings()
        {
            return _warnings;
        }

        public static List<Log> GetAll()
        {
            return _allLogs;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Description : Convert a unity logtype into a Console service log type
        /// </summary>
        /// <param name="pType"></param>
        /// <returns></returns>
        public static eLogType ConvertLogType(LogType pType)
        {
            eLogType lResult = eLogType.NONE;

            switch(pType)
            {
                case LogType.Log:
                    lResult = eLogType.LOG;
                    break;

                case LogType.Warning:
                    lResult = eLogType.WARNING;
                    break;

                case LogType.Assert:
                    lResult = eLogType.ASSERT;
                    break;

                case LogType.Error:
                    lResult = eLogType.ERROR;
                    break;

                case LogType.Exception:
                    lResult = eLogType.EXCEPTION;
                    break;
            }

            return lResult;
        }

        #endregion
    }
}
