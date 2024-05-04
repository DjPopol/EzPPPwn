namespace Ez_PPPwn
{
    public class FirmwaresScriptInfos
    {
        public FirmwaresScriptInfos() { }
        public FirmwaresScriptInfos(string defaultFirmware, string[] firmwares)
        {
            DefaultFirmware = defaultFirmware;
            Firmwares = firmwares;  
        }
        public readonly string DefaultFirmware = string.Empty;
        public readonly string[] Firmwares = [];
    }
}
