namespace Ez_PPPwn
{
    public class ExecuteInfos(string pathToScript, string pathToOffsets, string networkInterface, string firmware, string stage1Path, string stage2Path)
    {
        public readonly string PathToScript = pathToScript;
        public readonly string PathToOffsets = pathToOffsets;
        public readonly string NetworkInterface = networkInterface;
        public readonly string Firmware = firmware;
        public readonly string Stage1Path = stage1Path;
        public readonly string Stage2Path = stage2Path;
    }
}
