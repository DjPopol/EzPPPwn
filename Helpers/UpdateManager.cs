using DpLib.Enums;
using DpLib.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DpLib.Helpers
{
    public class UpdateManager(string apiBaseUrl, string accessToken)
    {
        string ApiBaseUrl = apiBaseUrl;
        HttpClient _httpClient = new();
        readonly CancellationTokenSource _cts = new();

        public async Task<ReleaseInfos> GetLastReleaseInfosAsync()
        {
            SetHttpClient();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{ApiBaseUrl}/latest");
                response.EnsureSuccessStatusCode(); // Throw exception if not successful

                string jsonContent = await response.Content.ReadAsStringAsync();
                var releaseInfo = ParseReleaseInfos(jsonContent);

                return releaseInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving last release: {ex.Message}");
                return new();
            }
        }

        static ReleaseInfos ParseReleaseInfos(string jsonContent)
        {
            // Deserialize JSON directly into ReleaseInfos properties
            ReleaseInfos releaseInfos = new ();

            using (JsonDocument document = JsonDocument.Parse(jsonContent))
            {
                var root = document.RootElement;
                string? nameElement = root.GetProperty("name").GetString();
                if (nameElement != null)
                {
                    releaseInfos.Name = nameElement;
                }
                string? tagElement = root.GetProperty("tag_name").GetString();
                if (tagElement != null)
                {
                    releaseInfos.Version = ParseVersionWithTagName(tagElement);
                }

                if (releaseInfos.Version != null && releaseInfos.Version != new Version(0, 0, 0, 0))
                {
                    var assets = root.GetProperty("assets");
                    if (assets.GetArrayLength() > 0)
                    {
                        string? downloadUrlElement = assets[0].GetProperty("browser_download_url").GetString();
                        if (downloadUrlElement != null)
                        {
                            releaseInfos.DownloadURL = downloadUrlElement;
                        }
                    }
                }
            }

            return releaseInfos;
        }
        public static Version ParseVersion(string versionString)
        {
            int major = 0, minor = 0, build = 0, revision = 0;

            // Split versionString by '.' to get major, minor, build, and revision parts
            string[] parts = versionString.Split('.');
            if (parts.Length >= 1)
            {
                _ = int.TryParse(parts[0], out major);
                if (parts.Length >= 2)
                {
                    _ = int.TryParse(parts[1], out minor);

                    // Check if the third part exists (build)
                    if (parts.Length >= 3)
                    {
                        _ = int.TryParse(parts[2], out build);
                    }

                    // Check if the fourth part exists (revision)
                    if (parts.Length >= 4)
                    {
                        _ = int.TryParse(parts[3], out revision);
                    }
                }
            }
            else
            {
                // Handle invalid version format
                Console.WriteLine("Invalid version format");
            }
            return new Version(major, minor, build, revision);
        }

        public static Version ParseVersionWithTagName(string tagName)
        {
            const string VersionPrefix = "#v";

            if (!string.IsNullOrEmpty(tagName))
            {
                int startIndex = tagName.IndexOf(VersionPrefix, StringComparison.OrdinalIgnoreCase);
                if (startIndex != -1)
                {
                    startIndex += VersionPrefix.Length;
                    int endIndex = tagName.IndexOf('#', startIndex);
                    if (endIndex == -1)
                    {
                        endIndex = tagName.Length;
                    }

                    if (endIndex > startIndex)
                    {
                        string versionString = tagName[startIndex..endIndex];
                        return ParseVersion(versionString);
                    }
                }
            }
            return new Version();
        }

        public async Task<Version> GetLastVersionAsync()
        {
            try
            {
                ReleaseInfos? releaseInfo = await GetLastReleaseInfosAsync();
                if (releaseInfo != null)
                {
                    return releaseInfo.Version;
                }
                else
                {
                    // If no release information is available, return an empty version or handle as needed
                    return new Version(); // This assumes your Version class has a default constructor
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting latest version.", ex);
            }
        }
        public async Task<List<ReleaseInfos>?> GetReleasesAsync()
        {
            SetHttpClient();
            HttpResponseMessage response = await _httpClient.GetAsync(ApiBaseUrl);
            response.EnsureSuccessStatusCode(); // Throw exception if not successful

            using Stream? responseStream = await response.Content.ReadAsStreamAsync();
            if (responseStream == null)
            {
                return null;
            }
            return await JsonSerializer.DeserializeAsync<List<ReleaseInfos>>(responseStream);
        }

        static async Task DownloadAndInstallUpdateAsync(ReleaseInfos releaseInfos, IProgress<DownloadProgress> progress)
        {
            DownloadProgress downloadProgress = new()
            {
                Status = DOWNLOAD_PROGRESS_STATUS.STARTING,
                BytesRead = 0,
                TotalBytes = 0,
                ErrorMessage = string.Empty,
                Filename = releaseInfos.DownloadURL // Example filename
            };

            try
            {
                progress.Report(downloadProgress);

                // Simulated download process
                long totalBytes = 10000; // Example total size in bytes
                long bytesRead = 0;
                Random random = new();

                downloadProgress.Status = DOWNLOAD_PROGRESS_STATUS.DOWNLOADING;
                downloadProgress.TotalBytes = totalBytes;

                while (bytesRead < totalBytes)
                {
                    // Simulate progress
                    await Task.Delay(100); // Simulate async download delay

                    bytesRead += random.Next(100, 1000); // Simulate bytes read
                    downloadProgress.BytesRead = bytesRead;

                    progress.Report(downloadProgress);
                }

                // Simulate completion
                downloadProgress.Status = DOWNLOAD_PROGRESS_STATUS.COMPLETED;
                progress.Report(downloadProgress);

                // Install logic after download completes
                // Replace this with actual installation logic
                Console.WriteLine("Download complete. Installing update...");

                // Simulate installation delay
                await Task.Delay(1000);

                Console.WriteLine("Update installed successfully!");
            }
            catch (OperationCanceledException)
            {
                downloadProgress.Status = DOWNLOAD_PROGRESS_STATUS.CANCELED;
                progress.Report(downloadProgress);
            }
            catch (Exception ex)
            {
                downloadProgress.Status = DOWNLOAD_PROGRESS_STATUS.FAILED;
                downloadProgress.ErrorMessage = ex.Message;
                progress.Report(downloadProgress);
            }
        }

        public void CancelUpdateCheck()
        {
            _cts.Cancel();
        }
        void SetHttpClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "EzPayloadSender");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}