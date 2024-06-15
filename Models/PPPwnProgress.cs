using EzPPPwn.Enums;

namespace EzPPPwn.Models
{
    public class PPPwnProgress
    {
        public PPPwnProgress() { }
        public string ConsoleMessage = string.Empty;
        public string ErrorMessage = string.Empty;
        public string Message = string.Empty;
        public object? Result = null;
        public double? Progress = null;
        public double? ProgressMax = null;
        public PPPWN_PROGESS_STATUS Status = PPPWN_PROGESS_STATUS.NONE;
    }
}
