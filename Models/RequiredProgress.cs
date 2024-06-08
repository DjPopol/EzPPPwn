using EzPPPwn.Enums;

namespace EzPPPwn.Models
{
    public class RequiredProgress
    {
        public RequiredProgress() { }
        // textBoxConsole
        public string ConsoleMessage = string.Empty;
        // labelCurrentStatus
        public string CurrentStatus = string.Empty;
        // Max progressBarCurrent
        public long? CurrentProgressMax = null;
        // Value progressBarCurrent
        public long? CurrentProgress = null;
        // CustomText progressBarCurrent
        public string CurrentProgressStatus = string.Empty;
        // MessageBox if not empty
        public string ErrorMessage = string.Empty;
        // CustomText progressBarMain
        public string MainProgressStatus = string.Empty;
        // Max progressBarMain
        public int? MainProgressMax = null;
        // Value progressBarMain
        public int? MainProgress = null;
        // labelMainStatus 
        public string MainStatus = string.Empty;
        public REQUIRED_PROGESS_STATUS Status = REQUIRED_PROGESS_STATUS.NONE;
    }
}
