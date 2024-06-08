using EzPPPwn.Models;

namespace EzPPPwn.Helpers
{
    public class PPPwnOptions(bool useAutoRetry, bool useGroomDelay, int groomDelay, bool useNoWaitPADI, bool useRealSleep, bool useTimeOut, int timeOut, bool useWaitAfterPIN, int waitAfterPIN)
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
                WaitAfterPIN = WaitAfterPIN
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
                jsonPPPwnConfig.WaitAfterPIN);
        }
        #endregion
    }
}
