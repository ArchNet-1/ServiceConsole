using UnityEditor;
using UnityEngine;

namespace ArchNet.Service.Console.Editor
{
    /// <summary>
    /// [SERVICE] - [ARCH NET] - [CONSOLE] : Advanced Settings Editor Window
    /// @author : LOUIS PAKEL
    /// </summary>
    public class AdvancedSettingsWindow : EditorWindow
    {
        #region Private Fields

        // Debug Priority Level
        private bool _feature = true;
        private bool _important = true;
        private bool _methods = true;
        private bool _loop = true;
        private bool _custom = true;

        #endregion

        #region Unity Methods

        private void OnGUI()
        {
            GUIStyle searchingStyle = new GUIStyle();
            Texture2D searchingTexture = new Texture2D(70, 100);

            searchingTexture.SetPixel(70, 100, Color.white);
            searchingStyle.normal.background = searchingTexture;

            DrawPriority();
            ApplyFilter();

            if (GUILayout.Button("Close"))
            {
                this.Close();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Description : Draw all priority
        /// </summary>
        private void DrawPriority()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("Advanced Settings", EditorStyles.wordWrappedLabel);
            EditorGUILayout.Space(20);
            _feature = GUILayout.Toggle(ConsoleService.GetAdvancedSettings().feature, new GUIContent("Feature"), GUILayout.Width(70));

            _important = GUILayout.Toggle(ConsoleService.GetAdvancedSettings().important, new GUIContent("Important"), GUILayout.Width(70));

            _methods = GUILayout.Toggle(ConsoleService.GetAdvancedSettings().methods, new GUIContent("Methods"), GUILayout.Width(70));

            _loop = GUILayout.Toggle(ConsoleService.GetAdvancedSettings().loop, new GUIContent("Loop"), GUILayout.Width(70));

            _custom = GUILayout.Toggle(ConsoleService.GetAdvancedSettings().custom, new GUIContent("Custom"),  GUILayout.Width(70));
            EditorGUILayout.Space(20);
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Description : Drawn a custom console
        /// </summary>
        private void ApplyFilter()
        {
            ConsoleService.GetAdvancedSettings().feature = _feature;
            ConsoleService.GetAdvancedSettings().important = _important;
            ConsoleService.GetAdvancedSettings().methods = _methods;
            ConsoleService.GetAdvancedSettings().loop = _loop;
            ConsoleService.GetAdvancedSettings().custom = _custom;
        }

        #endregion
    }
}
