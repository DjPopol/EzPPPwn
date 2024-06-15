using EzPPPwn.Enums;

namespace EzPPPwn.Models
{
    public class DownloadProgress
    {
        public DownloadProgress() { }
        public long BytesRead;
        public string ErrorMessage = string.Empty;
        public string Filename = string.Empty;
        public double Percentage => TotalBytes > 0 ? (double)BytesRead / TotalBytes * 100 : 0;
        public DOWNLOAD_PROGRESS_STATUS Status = DOWNLOAD_PROGRESS_STATUS.NONE;
        public long TotalBytes;
    }
}
