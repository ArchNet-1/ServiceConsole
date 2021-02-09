using System;

namespace ArchNet.Service.Console.Model
{
    /// <summary>
    /// [SERVICE] - [ARCH NET] - [CONSOLE] : Model class Advanced settings
    /// @author : LOUIS PAKEL
    /// </summary>
    [Serializable]
    public class AdvancedSettings
    {
        #region Properties

        public bool feature = false;
        public bool important = true;
        public bool methods = true;
        public bool loop = true;
        public bool custom = true;

        #endregion
    }
}
