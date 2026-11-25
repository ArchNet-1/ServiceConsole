// ArchNet · Console Service — Which priorities the Console Service window shows.
// Docs: https://archnet-plateform.fr/#ServiceConsole

using System;

namespace ArchNet.Service
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
