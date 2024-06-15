using DpLib.Enums;

namespace DpLib.Models
{
    public class DownloadProgress
    {
        public long BytesRead { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public double Percentage => TotalBytes > 0 ? (double)BytesRead / TotalBytes * 100 : 0;
        public DOWNLOAD_PROGRESS_STATUS Status { get; set; } = DOWNLOAD_PROGRESS_STATUS.NONE;
        public long TotalBytes { get; set; }
    }
}
