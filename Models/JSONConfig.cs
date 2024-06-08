using System.Text.Json.Serialization;

namespace EzPPPwn.Models
{
    public class JSONConfig
    {
        public required string Interface { get; set; }
        public required string Firmware { get; set; }
        public required bool ShowConsole { get; set; }
        [JsonPropertyName("Stage2")]
        public required string Stage2Path { get; set; }
        [JsonPropertyName("PPPwn")]
        public required JSONPPPwnOptions JSON_PPPwnConfig { get; set; }

    }
}
