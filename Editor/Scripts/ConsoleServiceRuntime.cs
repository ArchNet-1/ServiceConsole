using ArchNet.Service.Console;
using UnityEngine;

namespace Sportfaction.CasualFantasy.Services.ServiceDebug
{
    /// <summary>
    /// [SERVICE] - [ARCH NET] - [CONSOLE] : Console Runtime
    /// @author : LOUIS PAKEL
    /// </summary>
    public class ConsoleServiceRuntime : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Canvas Group")]
        [SerializeField, Tooltip("Log")]
        private CanvasGroup _log;
        [SerializeField, Tooltip("Warning")]
        private CanvasGroup _warning;
        [SerializeField, Tooltip("Error")]
        private CanvasGroup _error;

        #endregion

        #region Public Methods

        /// <summary>
        /// Description : Initiate Canvas Console Service for mobile
        /// </summary>
        public void Init()
        {
            // Init Log canvas
            _log.alpha = 1;
            _log.blocksRaycasts = false;
            _log.interactable = false;

            // Init Warning canvas
            _warning.alpha = 1;
            _warning.blocksRaycasts = false;
            _warning.interactable = false;

            // Init Error canvas
            _error.alpha = 1;
            _error.blocksRaycasts = false;
            _error.interactable = false;
        }

        #endregion

        #region Toggles

        /// <summary>
        /// Description : Display / Hide Logs message
        /// </summary>
        public void ToggleLogs()
        {
            if (ConsoleService.GetLogEvent() == true)
            {
                ConsoleService.SetLogEvent(false);

                Debug.Log("[DEBUG SERVICE] - [LOG EVENT] Disable");

                _log.alpha = 0;
            }
            else
            {
                ConsoleService.SetLogEvent(true);

                Debug.Log("[DEBUG SERVICE] - [LOG EVENT] Activate");

                _log.alpha = 1;
            }
        }

        /// <summary>
        /// Description : Display / Hide Warnings message
        /// </summary>
        public void ToggleWarnings()
        {
            if (ConsoleService.GetWarningEvent() == true)
            {
                ConsoleService.SetWarningEvent(false);

                Debug.Log("[DEBUG SERVICE] - [WARNING EVENT] Disable");

                _warning.alpha = 0;
            }
            else
            {
                ConsoleService.SetWarningEvent(true);

                Debug.Log("[DEBUG SERVICE] - [WARNING EVENT] Activate");
                _warning.alpha = 1;
            }
        }

        /// <summary>
        /// Description : Display / Hide Errors message
        /// </summary>
        public void ToggleErrors()
        {
            if(ConsoleService.GetErrorEvent() == true)
            {
                ConsoleService.SetErrorEvent(false);

                Debug.Log("[DEBUG SERVICE] - [ERROR EVENT] Disable");

                _error.alpha = 0;
            }
            else
            {
                ConsoleService.SetErrorEvent(true);

                Debug.Log("[DEBUG SERVICE] - [ERROR EVENT] Activate");

                _error.alpha = 1;
            }
        }

        #endregion
    }
}
