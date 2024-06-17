using EzPPPwn.Enums;
using EzPPPwn.Models;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace EzPPPwn.Helpers
{
    public static class Tools
    {
        public static readonly string PathApp = Environment.CurrentDirectory;
        public static readonly string PathTmp = Path.Combine(Path.GetTempPath(), "EzPPPwn");
        public static Config MyConfig = new();
        #region FUNCTIONS
        public static void DeletePathTmp()
        {
            if (Directory.Exists(PathTmp))
            {
                Directory.Delete(PathTmp, true);
            }
        }
        public static OsInfos GetOsInfos()
        {
            WINDOWS_VERSION windowsVersion = WINDOWS_VERSION.NOT_WINDOWS;
            Architecture architecture = Architecture.X86;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var version = Environment.OSVersion.Version;

                if (version.Major == 6 && version.Minor == 1)
                {
                    windowsVersion = WINDOWS_VERSION.WINDOWS7;
                }
                else if (version.Major == 6 && (version.Minor == 2 || version.Minor == 3))
                {
                    windowsVersion = WINDOWS_VERSION.WINDOWS8;
                }
                else if (version.Major == 10)
                {
                    windowsVersion = WINDOWS_VERSION.WINDOWS10;
                }
                else if (version.Major == 11)
                {
                    windowsVersion = WINDOWS_VERSION.WINDOWS11;
                }
                else
                {
                    windowsVersion = WINDOWS_VERSION.OTHER;
                }
                architecture = RuntimeInformation.OSArchitecture;
            }
            return new(windowsVersion, architecture);
        }
        public static string GetStringBetween(string text, string start, string end = "")
        {
            if (end == string.Empty || end == "")
            {
                end = start;
            }
            int startIndex = text.IndexOf(start) + start.Length;
            string str = text[startIndex..];
            int endIndex = str.IndexOf(end);
            return str[..endIndex];
        }
        public static string GetTitle()
        {
            return $"Ez PPPwn v{GetVersion()} by DjPopol";
        }
        public static string GetToken()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string? assemblyName = assembly.GetName().Name;
            if (assemblyName != null)
            {
                string resourceName = $"{assemblyName}.Resources.token.dp"; // Replace YourNamespace with your project's namespace

                using Stream stream = assembly.GetManifestResourceStream(resourceName) ?? throw new InvalidOperationException("Token file not found.");
                using StreamReader reader = new(stream);
                return reader.ReadToEnd().Trim();
            }
            return string.Empty;
        }
        public static Version GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return assembly.GetName().Version;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }
        public static string GetVersionStr()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version? version = assembly.GetName().Version;
            return version != null ? version.Major + "." + version.Minor + version.Build : string.Empty;
        }
        public static async Task<bool> IsConnectedToInternetAsync()
        {
            try
            {
                using HttpClient client = new ();
                client.Timeout = TimeSpan.FromSeconds(5);
                HttpResponseMessage response = await client.GetAsync("https://www.google.com");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public static string ReplaceStringBetween(string text, string replace, string start, string end = "")
        {
            if (end == string.Empty || end == "")
            {
                end = start;
            }
            int startIndex = text.IndexOf(start) + start.Length;
            string str = text[startIndex..];
            int endIndex = str.IndexOf(end);

            string strBetween = str[..endIndex];
            return text.Replace(strBetween, replace);
        }
        public static int Size2Bytes(double size, string unit)
        {
            int result = 0;
            if (unit != string.Empty)
            {
                switch (unit.ToUpper())
                {
                    case "TB":
                        result = (int)Math.Round(size * Math.Pow(1000.0, 4), 0);
                        break;
                    case "GB":
                        result = (int)Math.Round(size * Math.Pow(1000.0, 3));
                        break;
                    case "MB":
                        result = (int)Math.Round(size * Math.Pow(1000.0, 2));
                        break;
                    case "KB":
                        result = (int)Math.Round(size * 1000.0);
                        break;
                    case "B":
                        result = (int)size;
                        break;
                }
            }
            return result;
        }
        public static bool StopAllProcessesByName(string processName)
        {
            try
            {
                var processes = Process.GetProcessesByName(processName);
                foreach (var process in processes)
                {
                    process.Kill();
                    process.WaitForExit();
                }

                Debug.WriteLine($"Tous les processus '{processName}' ont été arrêtés avec succès.");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Une erreur est survenue : {ex.Message}");
                return false;
            }
        }
        #endregion
    }
}
