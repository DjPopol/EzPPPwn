namespace EzPPPwn.Models
{
    // Helper to have Fw : '900' FwWithPoint : '9.00' or Fw : '1100' FwWithPoint : '11.00' 
    public class Firmware(string firmware)
    {
        public static readonly string[] SupportedFws = ["7.00", "7.01", "7.02", "7.50", "7.51", "7.55", "8.00", "8.01", "8.03", "8.50", "8.52", "9.00", "9.03", "9.04", "9.50", "9.51", "9.60", "10.00", "10.01", "10.50", "10.70", "10.71", "11.00"];
        public string Fw { get; private set; } = firmware.Replace(".", "").Trim();
        public string FwWithPoint => Fw.Insert(Fw.Length == 3 ? 1 : 2, ".");
    }
}
