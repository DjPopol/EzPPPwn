using System.Runtime.InteropServices;
using EzPPPwn.Enums;

namespace EzPPPwn.Models
{
    public class OsInfos(WINDOWS_VERSION version, Architecture architecture)
    {
        public readonly WINDOWS_VERSION Version = version;
        public readonly Architecture Architecture = architecture;
    }
}
