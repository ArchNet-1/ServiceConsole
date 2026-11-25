// ArchNet · Console Service — on-screen panels
// For builds without the Editor (mobile…): three CanvasGroups that show or hide the log,
// warning and error panels. Hook ToggleLogs / ToggleWarnings / ToggleErrors to buttons.
// Docs: https://archnet-plateform.fr/#ServiceConsole

using UnityEngine;

namespace ArchNet.Service
{
    [AddComponentMenu("ArchNet/Console Service Runtime")]
    public class ConsoleServiceRuntime : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] CanvasGroup _log;
        [SerializeField] CanvasGroup _warning;
        [SerializeField] CanvasGroup _error;

        /// <summary>Shows the three panels without letting them catch clicks.</summary>
        public void Init()
        {
            foreach (CanvasGroup panel in new[] { _log, _warning, _error })
            {
                if (panel == null) continue;
                panel.alpha = 1f;
                panel.blocksRaycasts = false;
                panel.interactable = false;
            }
        }

        public void ToggleLogs() => ConsoleService.SetLogEvent(Toggle(_log, ConsoleService.GetLogEvent()));
        public void ToggleWarnings() => ConsoleService.SetWarningEvent(Toggle(_warning, ConsoleService.GetWarningEvent()));
        public void ToggleErrors() => ConsoleService.SetErrorEvent(Toggle(_error, ConsoleService.GetErrorEvent()));

        static bool Toggle(CanvasGroup panel, bool shown)
        {
            bool show = !shown;
            if (panel != null) panel.alpha = show ? 1f : 0f;
            return show;
        }
    }
}
