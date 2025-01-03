namespace EzPPPwn.Models
{
    public class JSONPPPwnOptions
    {
        public required bool UseAutoRetry { get; set; }
        public required bool UseGroomDelay { get; set; }
        public required int GroomDelay { get; set; }
        public required bool UseNoWaitPADI { get; set; }
        public required bool UseRealSleep { get; set; }
        public required bool UseTimeOut { get; set; }
        public required int TimeOut { get; set; }
        public required bool UseWaitAfterPIN { get; set; }
        public required int WaitAfterPIN { get; set; }
        public required bool UseOldIpv6 { get; set; }
        public required bool UseBufferSize { get; set; }
        public required int BufferSize { get; set; }
    }
}
