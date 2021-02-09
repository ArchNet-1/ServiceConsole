using ArchNet.Service.Console.Enum;
using ArchNet.Service.Console.Model;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ArchNet.Service.Console.Editor
{
    /// <summary>
    /// Custom Console Service for editor debug 
    /// @author: Louis Pakel
    /// </summary>
    class ConsoleServiceEditor : EditorWindow
    {
        #region Private fields

        // Rect Panels
        private Rect upperPanel;
        private Rect lowerPanel;
        private Rect resizer;
        private Rect menuBar;

        private float sizeRatio = 0.75f;
        private bool isResizing;

        private float resizerHeight = 5f;
        private float menuBarHeight = 20f;

        // Menu Options
        private bool _defaultConsole = true;

        private string _searching = "";




        private bool _showLog = true;
        private bool _showWarnings = true;
        private bool _showErrors = true;

        // Scrolls
        private Vector2 upperPanelScroll;
        private Vector2 lowerPanelScroll;

        // Style
        private GUIStyle resizerStyle;
        private GUIStyle boxStyle;
        private GUIStyle searchingStyle;
        private GUIStyle textAreaStyle;

        // Texture and design
        private Texture2D boxBgOdd;
        private Texture2D boxBgEven;
        private Texture2D boxBgSelected;
        private Texture2D icon;
        private Texture2D errorIcon;
        private Texture2D errorIconSmall;
        private Texture2D warningIcon;
        private Texture2D warningIconSmall;
        private Texture2D infoIcon;
        private Texture2D infoIconSmall;
        private Texture2D searchingTexture;

        private List<Log> _logsLoaded;

        private bool _isOdd = false;

        #endregion

        [MenuItem("ArchNet/Services/Console")]

        static void Init()
        {
            ConsoleServiceEditor window = (ConsoleServiceEditor)EditorWindow.GetWindow(typeof(ConsoleServiceEditor), true, "ArchNet Console");
            window.Show();
        }

        #region OnEvent

        private void OnEnable()
        {
            // load icons
            errorIcon = EditorGUIUtility.Load("icons/console.erroricon.png") as Texture2D;
            warningIcon = EditorGUIUtility.Load("icons/console.warnicon.png") as Texture2D;
            infoIcon = EditorGUIUtility.Load("icons/console.infoicon.png") as Texture2D;

            // Load console icons
            errorIconSmall = EditorGUIUtility.Load("icons/console.erroricon.sml.png") as Texture2D;
            warningIconSmall = EditorGUIUtility.Load("icons/console.warnicon.sml.png") as Texture2D;
            infoIconSmall = EditorGUIUtility.Load("icons/console.infoicon.sml.png") as Texture2D;

            // init resizer
            resizerStyle = new GUIStyle();
            resizerStyle.normal.background = EditorGUIUtility.Load("icons/d_AvatarBlendBackground.png") as Texture2D;

            // init box log 
            boxStyle = new GUIStyle();
            boxStyle.normal.textColor = new Color(0.7f, 0.7f, 0.7f);

            // load unity console style
            boxBgOdd = EditorGUIUtility.Load("builtin skins/darkskin/images/cn entrybackodd.png") as Texture2D;
            boxBgEven = EditorGUIUtility.Load("builtin skins/darkskin/images/cnentrybackeven.png") as Texture2D;
            boxBgSelected = EditorGUIUtility.Load("builtin skins/darkskin/images/menuitemhover.png") as Texture2D;

            // set text log style
            textAreaStyle = new GUIStyle();
            textAreaStyle.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
            textAreaStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/projectbrowsericonareabg.png") as Texture2D;

            searchingStyle = new GUIStyle();
            searchingTexture = new Texture2D(70, 100);
   
            searchingTexture.SetPixel(70, 100, Color.white);
            searchingStyle.normal.background = searchingTexture;

            // Set Selected Log to null
            ConsoleService.SetSelectedLog(null);

            _logsLoaded = new List<Log>();

            ConsoleService.Reset();

            ConsoleService.Test();

            Application.logMessageReceived += LogMessageReceived;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= LogMessageReceived;
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= LogMessageReceived;
        }



        void OnGUI()
        {
            DrawMenuBar();
            DrawUpperPanel();
            DrawLowerPanel();
            DrawResizer();

            ProcessEvents(Event.current);

            EditorUtility.SetDirty(this);

            if (GUI.changed) Repaint();
        }


        #endregion


        #region Drawer

        /// <summary>
        /// Description : Draw the NavBar Menu of the console
        /// </summary>
        private void DrawMenuBar()
        {
            // Init Nav bar menu rect
            menuBar = new Rect(0, 0, position.width, menuBarHeight);

            // Design Nav Bar Menu Area
            GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
            GUILayout.BeginHorizontal();

            DrawAdvancedSettings();

            // Create Console default toggles
            if (GUILayout.Button(new GUIContent("Clear"), EditorStyles.toolbarButton, GUILayout.Width(70)))
            {
                // DELETE LOGS
                _logsLoaded = new List<Log>();
                ConsoleService.Reset();
            }

            _defaultConsole = GUILayout.Toggle(_defaultConsole, new GUIContent("Default Console"), EditorStyles.toolbarButton, GUILayout.Width(150));

            GUILayout.FlexibleSpace();

            GUILayout.Label("Searching : ",EditorStyles.toolbarButton, GUILayout.Width(100));

            _searching = GUILayout.TextField(_searching, 70, searchingStyle, GUILayout.Width(70), GUILayout.Height(70));

            // Create log Type toggles
            _showLog = GUILayout.Toggle(_showLog, new GUIContent(" Logs", infoIconSmall), EditorStyles.toolbarButton, GUILayout.Width(70));
            _showWarnings = GUILayout.Toggle(_showWarnings, new GUIContent(" Warnings", warningIconSmall), EditorStyles.toolbarButton, GUILayout.Width(70));
            _showErrors = GUILayout.Toggle(_showErrors, new GUIContent(" Errors", errorIconSmall), EditorStyles.toolbarButton, GUILayout.Width(70));

            // Close Design Nav Bar Menu Area
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }


        #region Filters



        /// <summary>
        /// Description : Drawer Advanced Settings
        /// </summary>
        private void DrawAdvancedSettings()
        {
            if (GUILayout.Button(new GUIContent("[Advanced Settings]"), EditorStyles.toolbarButton, GUILayout.Width(150)))
            {
                AdvancedSettingsWindow window = ScriptableObject.CreateInstance<AdvancedSettingsWindow>();
                window.position = new Rect(Screen.width / 2, Screen.height / 2 + 160, 250, 250);
                window.ShowPopup();
            }
        }



        #endregion

        #region Panels

        /// <summary>
        /// Description : Drawer for the logs messages in the upper panel  area
        /// </summary>
        private void DrawUpperPanel()
        {
            // design Upper panel area
            upperPanel = new Rect(0, menuBarHeight, position.width, (position.height * sizeRatio) - menuBarHeight);

            GUILayout.BeginArea(upperPanel);
            
            // Add a scroll
            upperPanelScroll = GUILayout.BeginScrollView(upperPanelScroll);

            // Draw console
            DrawDefaultConsole();
            DrawCustomLogs(ConsoleService.GetAll());

            // CLose Design
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }


        /// <summary>
        /// Description : Drawer for the log detail , in the lower area panel
        /// </summary>
        private void DrawLowerPanel()
        {
            // Design Lower panel area
            lowerPanel = new Rect(0, (position.height * sizeRatio) + resizerHeight, position.width, (position.height * (1 - sizeRatio)) - resizerHeight);

            GUILayout.BeginArea(lowerPanel);
            
            // Add a scroll
            lowerPanelScroll = GUILayout.BeginScrollView(lowerPanelScroll);


            // Load Selected Log
            if (ConsoleService.GetSelectedLog() != null)
            {
                GUILayout.TextArea(ConsoleService.GetSelectedLog().GetLog(), textAreaStyle);
            }


            // Close Design
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }


        #endregion

        #region Logs

        private void DrawDefaultConsole()
        {
            if (_defaultConsole == true)
            {
                bool lOdd = false;

                // Load all logs
                for (int i = 0; i < _logsLoaded.Count; i++)
                {
                    if(_logsLoaded[i].GetTitle().Contains(_searching) == false)
                    {
                        return;
                    }

                    lOdd = !lOdd;
                    if (DrawBox(_logsLoaded[i]) == true)
                    {
                        if (ConsoleService.GetSelectedLog() != null)
                        {
                            ConsoleService.GetSelectedLog().IsSelected = false;
                        }

                        _logsLoaded[i].IsSelected = true;
                        ConsoleService.SetSelectedLog(_logsLoaded[i]);
                        GUI.changed = true;
                    }
                    _isOdd = !_isOdd;
                }
            }
        }

        #region Draw Custom Console

      
        /// <summary>
        ///  Description : Draw custom Logs from eLog type
        /// </summary>
        /// <param name="pLogs"></param>
        private void DrawCustomLogs(List<Log> pLogs)
        {
            for (int i = 0; i < pLogs.Count; i++)
            {
                if (DrawPriotiry(pLogs[i]) == true)
                {
                    if (pLogs[i].GetTitle().Contains(_searching) == true)
                    {
                        if (DrawBox(pLogs[i]) == true)
                        {
                            if (ConsoleService.GetSelectedLog() != null)
                            {
                                ConsoleService.GetSelectedLog().IsSelected = false;
                            }

                            pLogs[i].IsSelected = true;
                            ConsoleService.SetSelectedLog(pLogs[i]);
                            GUI.changed = true;
                        }

                        _isOdd = !_isOdd;
                    }
                }
            }
        }


        /// <summary>
        ///  Description : Draw log if its a priority active
        /// </summary>
        /// <param name="pLog"></param>
        /// <returns></returns>
        private bool DrawPriotiry(Log pLog)
        {
            if (ConsoleService.GetAdvancedSettings().feature == true && pLog.GetLogPriority() == eLogPriority.FEATURE)
            {
                return true;
            }
            // Draw Important ( Priority 2 )
            else if (ConsoleService.GetAdvancedSettings().important == true && pLog.GetLogPriority() == eLogPriority.IMPORTANT)
            {
                return true;
            }
            // Draw Methods ( Priority 3 )
            else if(ConsoleService.GetAdvancedSettings().methods == true && pLog.GetLogPriority() == eLogPriority.METHODS)
            {
                return true;
            }
            // Draw Loop ( Priority 4 )
            else if(ConsoleService.GetAdvancedSettings().loop == true && pLog.GetLogPriority() == eLogPriority.LOOP)
            {
                return true;
            }
            // Draw Custom ( Priority 5 )
            else if(ConsoleService.GetAdvancedSettings().custom == true && pLog.GetLogPriority() == eLogPriority.CUSTOM)
            {
                return true;
            }

            return false;
        }

   

        #endregion

        #endregion

        /// <summary>
        /// Description : Draw resizer for thelofs 
        /// </summary>
        private void DrawResizer()
        {
            // Design the resizer area
            resizer = new Rect(0, (position.height * sizeRatio) - resizerHeight, position.width, resizerHeight * 2);

            GUILayout.BeginArea(new Rect(resizer.position + (Vector2.up * resizerHeight), new Vector2(position.width, 2)), resizerStyle);
            GUILayout.EndArea();

            EditorGUIUtility.AddCursorRect(resizer, MouseCursor.ResizeVertical);
        }


        /// <summary>
        ///  Description : Draw a log in the upper panel ( has button for selected option)
        /// </summary>
        /// <param name="content"></param>
        /// <param name="isOdd"></param>
        /// <param name="isSelected"></param>
        /// <returns></returns>
        private bool DrawBox(Log pLog)
        {
            // Log selected
            if (pLog.IsSelected == true)
            {
                // Change background to selected
                boxStyle.normal.background = boxBgSelected;
            }
            else
            {
                // Log is Odd or not ( pair / impair )
                if (_isOdd == true)
                {
                    boxStyle.normal.background = boxBgOdd;
                }
                else
                {
                    boxStyle.normal.background = boxBgEven;
                }
            }

            // Load icon from Logtype
            switch (pLog.GetLogType())
            {
                case eLogType.ERROR: 
                    
                    if(_showErrors == false)
                    {
                        return false;
                    }

                    icon = errorIcon;break;

                case eLogType.EXCEPTION:

                    if (_showErrors == false)
                    {
                        return false;
                    }

                    icon = errorIcon; break;

                case eLogType.ASSERT:

                    if (_showErrors == false)
                    {
                        return false;
                    }

                    icon = errorIcon; break;

                case eLogType.WARNING:

                    if (_showWarnings == false)
                    {
                        return false;
                    }

                    icon = warningIcon; break;

                case eLogType.LOG:

                    if (_showLog == false)
                    {
                        return false;
                    }

                    icon = infoIcon; break;
            }

            boxStyle.normal.textColor =  new Color(pLog.GetColor().r / 255.0f, pLog.GetColor().g / 255.0f, pLog.GetColor().b / 255.0f);

            // Create the log has button for the option of the selection
            return GUILayout.Button(new GUIContent(pLog.GetTitle(), icon), boxStyle, GUILayout.ExpandWidth(true), GUILayout.Height(45));
        }

        #endregion

        /// <summary>
        /// Description : Manage event ( scroll, resizing )
        /// </summary>
        /// <param name="e"></param>
        private void ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0 && resizer.Contains(e.mousePosition))
                    {
                        isResizing = true;
                    }
                    break;

                case EventType.MouseUp:
                    isResizing = false;
                    break;
            }

            Resize(e);
        }

        /// <summary>
        /// Description : Resize an event
        /// </summary>
        /// <param name="e"></param>
        private void Resize(Event e)
        {
            if (isResizing)
            {
                sizeRatio = e.mousePosition.y / position.height;
                Repaint();
            }
        }

        /// <summary>
        /// Description : Load Log Message Received
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="stackTrace"></param>
        /// <param name="type"></param>
        private void LogMessageReceived(string condition, string stackTrace, LogType type)
        {
            Log lNewLog = new Log();
            lNewLog.SetId(_logsLoaded.Count + 1);
            lNewLog.SetTitle(condition);
            lNewLog.SetMessage(stackTrace);
            lNewLog.SetType(ConsoleService.ConvertLogType(type));

            _logsLoaded.Add(lNewLog);
        }

    }
}
