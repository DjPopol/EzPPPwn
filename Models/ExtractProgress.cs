using EzPPPwn.Enums;

namespace EzPPPwn.Models
{
    public class ExtractProgress
    {
        public ExtractProgress() { }
        public int TotalFiles = 0;
        public string Filename = string.Empty;
        public int FilesProcessed = 0;
        public double Percentage => TotalFiles > 0 ? (double)FilesProcessed / TotalFiles * 100 : 0.0;
        public long BytesRead = 0;
        public long TotalBytes = 0;
        public double BytesPercentage => TotalBytes > 0 && TotalBytes >= BytesRead ? (double)BytesRead / TotalBytes * 100 : BytesRead > TotalBytes ? 100.00 : 0.00;
        public string CurrentFile = string.Empty;
        public string ErrorMessage = string.Empty;
        public EXTRACT_PROGRESS_STATUS Status = EXTRACT_PROGRESS_STATUS.NONE;

    }
}
