using EzPPPwn.Models;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace EzPPPwn.Helpers
{
    public class Config
    {
        #region PROPERTIES
        public Firmware Firmware = new("11.00");
        private readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        public NetworkInterface? NetworkInterface = null;
        public PPPwnOptions PPPwnOptions = new(true, true,4,false,false,false,0,true,1);
        public static readonly string Path = System.IO.Path.Combine(Environment.CurrentDirectory, "EzPPPwn.json");
        public string Stage2Path = string.Empty;
        public bool ShowConsole = true;
        #endregion
        #region FUNCTIONS
        public bool CheckConfig()
        {
            bool result = true;
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (NetworkInterface == null || nics.First(i => i.Name == NetworkInterface.Name) == null)
            {
                result = false;
            }
            if (Firmware == null || Firmware.SupportedFws.First(i => i == Firmware.FwWithPoint) == null)
            {
                result = false;
            }
            if (Stage2Path == string.Empty || !File.Exists(Stage2Path))
            {
                result = false;
            }
            return result;
        }
        public bool Load()
        {
            try
            {
                if (File.Exists(Path))
                {
                    string jsonFile = File.ReadAllText(Path);
                    var result = JsonSerializer.Deserialize<JSONConfig>(jsonFile);
                    if (result != null && result is JSONConfig jsonConfig)
                    {
                        Firmware = new(jsonConfig.Firmware);
                        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                        if (nics == null || nics.Length < 1)
                        {
                            Console.WriteLine("MyConfig.LoadConfig : No network interfaces found.");
                        }
                        else
                        {
                            foreach (NetworkInterface networkInterface in nics)
                            {
                                if (networkInterface.Name == jsonConfig.Interface)
                                {
                                    NetworkInterface = networkInterface;
                                    break;
                                }
                            }
                        }
                        if (NetworkInterface == null)
                        {
                            Console.WriteLine($"MyConfig.LoadConfig : Network interfaces not found ({jsonConfig.Interface}.");
                        }
                        PPPwnOptions = PPPwnOptions.GetFromJSON(jsonConfig.JSON_PPPwnConfig);
                        Stage2Path = jsonConfig.Stage2Path;
                        ShowConsole = jsonConfig.ShowConsole;
                        return true;
                    }
                }
                return Save();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error MyConfig.LoadConfig() : {ex.Message}");
                return false;
            }
        }
        public bool Save()
        {
            try
            {
                if(NetworkInterface != null)
                {

                    JSONConfig jsonConfig = new() 
                    { 
                        Interface = NetworkInterface.Name,
                        Firmware = Firmware != null ? Firmware.FwWithPoint : string.Empty,
                        Stage2Path = Stage2Path,
                        JSON_PPPwnConfig = PPPwnOptions.GetJSON(),
                        ShowConsole = ShowConsole
                    };
                    string json = JsonSerializer.Serialize(jsonConfig, jsonSerializerOptions);
                    File.WriteAllText(Path, json);
                    return true;
                }
                return false;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine(jsonEx.Message);
                return false;
            }
        }
        public void Set(NetworkInterface? networkInterface, Firmware firmware, string stage2Path, PPPwnOptions pppwnOptions)
        {
            NetworkInterface = networkInterface;
            Firmware = firmware;
            Stage2Path = stage2Path;
            PPPwnOptions = pppwnOptions;  
        }
        #endregion
    }
}
