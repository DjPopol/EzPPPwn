
using Newtonsoft.Json;

namespace Ez_PPPwn
{
    public class Config(ExecuteInfos infos)
    {
        [JsonProperty("Config")]
        public ExecuteInfos Infos = infos;
    }
}
