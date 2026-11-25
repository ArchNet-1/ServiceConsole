// ArchNet · Console Service — Kind of a Console Service entry (log, warning, error…).
// Docs: https://archnet-plateform.fr/#ServiceConsole

namespace ArchNet.Service
{
    /// <summary>
    /// [SERVICE] - [ARCH NET] - [CONSOLE] : enum to refere all level of a log
    /// @author : LOUIS PAKEL
    /// </summary>
    public enum eLogType
    {
        NONE,
        LOG,
        ERROR,
        WARNING,
        ASSERT,
        EXCEPTION
    }
}
