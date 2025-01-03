using EzPPPwn.Models;

namespace EzPPPwn.Helpers
{
    public class PPPwnOptions(bool useAutoRetry, bool useGroomDelay, int groomDelay, bool useNoWaitPADI, bool useRealSleep, bool useTimeOut, int timeOut, bool useWaitAfterPIN, int waitAfterPIN, bool useOldIpv6, bool useBufferSize, int bufferSize)
    {
        #region PROPERTIES
        public bool UseAutoRetry = useAutoRetry;
        public bool UseGroomDelay = useGroomDelay;
        public int GroomDelay = groomDelay;
        public bool UseNoWaitPADI = useNoWaitPADI;
        public bool UseRealSleep = useRealSleep;
        public bool UseTimeOut = useTimeOut;
        public int TimeOut = timeOut;
        public bool UseWaitAfterPIN = useWaitAfterPIN;
        public int WaitAfterPIN = waitAfterPIN;
        public bool UseOldIpv6 = useOldIpv6;
        public bool UseBufferSize = useBufferSize;
        public int BufferSize = bufferSize;
        #endregion
        #region FUNCTION
        public JSONPPPwnOptions GetJSON()
        {
            return new()
            {
                UseAutoRetry = UseAutoRetry,
                UseGroomDelay = UseGroomDelay,
                GroomDelay = GroomDelay,
                UseNoWaitPADI = UseNoWaitPADI,
                UseRealSleep = UseRealSleep,
                UseTimeOut = UseTimeOut,
                TimeOut = TimeOut,
                UseWaitAfterPIN = UseWaitAfterPIN,
                WaitAfterPIN = WaitAfterPIN,
                UseOldIpv6 = UseOldIpv6 ,
                UseBufferSize = UseBufferSize,
                BufferSize = BufferSize
            };
        }
        public static PPPwnOptions GetFromJSON(JSONPPPwnOptions jsonPPPwnConfig)
        {
            return new(
                jsonPPPwnConfig.UseAutoRetry,
                jsonPPPwnConfig.UseGroomDelay,
                jsonPPPwnConfig.GroomDelay,
                jsonPPPwnConfig.UseNoWaitPADI,
                jsonPPPwnConfig.UseRealSleep,
                jsonPPPwnConfig.UseTimeOut,
                jsonPPPwnConfig.TimeOut,
                jsonPPPwnConfig.UseWaitAfterPIN,
                jsonPPPwnConfig.WaitAfterPIN,
                jsonPPPwnConfig.UseOldIpv6,
                jsonPPPwnConfig.UseBufferSize,
                jsonPPPwnConfig.BufferSize
                );
        }
        #endregion
    }
}
