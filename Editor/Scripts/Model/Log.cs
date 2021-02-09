using ArchNet.Service.Console.Enum;
using UnityEngine;

namespace ArchNet.Service.Console.Model
{
    /// <summary>
    /// [SERVICE] - [ARCH NET] - [CONSOLE] : Model class Log
    /// @author : LOUIS PAKEL
    /// </summary>
    public class Log
    {
        #region Private Fields

        // Unique Id of the log
        private int _id = 0;

        private eLogType _type = eLogType.NONE;

        private eLogPriority _priority = eLogPriority.NONE;

        // check if the bool is selected
        public bool IsSelected;

        private Color _color = new Color(75, 94, 224, 1);

        private string _title = "";

        // Content of the Log
        private string _message = "";

        #endregion

        #region Getter / Setter
        public void SetColor(Color pColor)
        {
            _color = pColor;
        }

        public void SetId(int pId)
        {
            if(pId > 0)
            {
                _id = pId;
            }
        }

        public void SetPriority(eLogPriority pPriority)
        {
            if (pPriority != eLogPriority.NONE)
            {
                _priority = pPriority;
            }
        }

        public void SetType(eLogType pType)
        {
            if(pType != eLogType.NONE)
            {
                _type = pType;
            }
        }

        public void SetTitle(string pTitle)
        {
            if(pTitle != "")
            {
                _title = pTitle;
            }
        }

        public void SetMessage(string pMessage)
        {
            if (pMessage != "")
            {
                _message = pMessage;
            }
        }

        public string GetMessage()
        {
            return _message;
        }

        public string GetTitle()
        {
            return _title;
        }

        public int GetId()
        {
            return _id;
        }

        public Color GetColor()
        {
            return _color;
        }

        public eLogType GetLogType()
        {
            return _type;
        }

        public eLogPriority GetLogPriority()
        {
            return _priority;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Description : Get Log
        /// </summary>
        /// <returns></returns>
        public string GetLog()
        {
            string lResult = "";

            lResult += "\n[" + GetTitle() + "] : " + GetMessage(); 

            return lResult;
        }

        /// <summary>
        /// Description : Get a log to string format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string lResult = "";

            lResult += "Id : " + GetId().ToString();
            lResult += "Type : " + GetLogType().ToString();
            lResult += "\n Title : " + GetTitle();
            lResult += "\n Message : " + GetMessage();
            lResult += "\n";

            return lResult;
        }

        /// <summary>
        /// Description : Check if the log given in parameter is equal to this log
        /// </summary>
        /// <param name="pLog"></param>
        /// <returns></returns>
        public bool IsEqual(Log pLog)
        {
            bool lResult = false;

            if (this.GetId() == pLog.GetId() &&
                this.GetLogType() == pLog.GetLogType()
                )
            {
                lResult = true;
            }

            return lResult;
        }

        #endregion
    }

}
