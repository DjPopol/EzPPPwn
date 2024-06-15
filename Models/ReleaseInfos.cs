namespace DpLib.Models
{
    public class ReleaseInfos
    {
        public string Name {get; set;} = string.Empty;
        public Version Version { get; set; } = new();
        public string DownloadURL { get; set; } = string.Empty;
    }
}
