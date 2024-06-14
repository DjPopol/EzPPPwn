using EzPPPwn.Enums;
using EzPPPwn.Extensions;
using EzPPPwn.Models;
using System.Diagnostics;

namespace EzPPPwn.Helpers
{
    public class RequiredTask(REQUIRED_JOBS[] jobs, IProgress<RequiredProgress>? callback = null, CancellationToken? cancellationToken = null)
    {
        public TASK_RESULT Result = TASK_RESULT.NONE;
        readonly REQUIRED_JOBS[] Jobs = jobs;
        readonly IProgress<RequiredProgress>? Callback = callback;
        #region FUNCTION
        static async Task<bool> DownloadNpcap(string destPath, IProgress<DownloadProgress>? progress = null, CancellationToken? cancellationToken = null)
        {
            try
            {
                if (!Directory.Exists(destPath))
                {
                    Directory.CreateDirectory(destPath);
                }
                return await Files.DownloadFileAsync("https://nmap.org/npcap/dist/npcap-1.79.exe", destPath, progress, cancellationToken ?? CancellationToken.None);
            }
            catch (Exception ex)
            {
                progress?.Report(new DownloadProgress() { Status = DOWNLOAD_PROGRESS_STATUS.FAILED, ErrorMessage = ex.ToString() });
                return false;
            }
        }
        static async Task<string> DownloadPPPwnCPP(string destPath, IProgress<DownloadProgress>? progress = null, CancellationToken? cancellationToken = null)
        {
            try
            {
                OsInfos osInfos = Tools.GetOsInfos();
                switch (osInfos.Version)
                {
                    case WINDOWS_VERSION.WINDOWS7:
                        if (osInfos.Architecture == System.Runtime.InteropServices.Architecture.X86)
                        {
                            if (await Files.DownloadFileAsync(
                                "https://nightly.link/xfangfang/PPPwn_cpp/workflows/ci.yaml/main/x86-windows-gnu%28win7%29.zip",
                                destPath, progress, cancellationToken ?? CancellationToken.None))
                            {
                                return "x86-windows-gnu(win7).zip";
                            }
                        }
                        else if (osInfos.Architecture == System.Runtime.InteropServices.Architecture.X64 ||
                            osInfos.Architecture == System.Runtime.InteropServices.Architecture.Arm64)
                        {
                            if (await Files.DownloadFileAsync(
                                "https://nightly.link/xfangfang/PPPwn_cpp/workflows/ci.yaml/main/x86_64-windows-gnu%28win7%29.zip",
                                destPath, progress, cancellationToken ?? CancellationToken.None))
                            {
                                return "x86_64-windows-gnu(win7).zip";
                            }
                        }
                        else
                        {
                            progress?.Report(new DownloadProgress() { Status = DOWNLOAD_PROGRESS_STATUS.FAILED, ErrorMessage = "Architecture not support" });
                        }
                        break;
                    case WINDOWS_VERSION.WINDOWS8:
                    case WINDOWS_VERSION.WINDOWS10:
                    case WINDOWS_VERSION.WINDOWS11:
                        if (osInfos.Architecture == System.Runtime.InteropServices.Architecture.X86)
                        {
                            if (await Files.DownloadFileAsync(
                                "https://nightly.link/xfangfang/PPPwn_cpp/workflows/ci.yaml/main/x86-windows-gnu.zip",
                                destPath, progress, cancellationToken ?? CancellationToken.None))
                            {
                                return "x86-windows-gnu.zip";
                            }
                        }
                        else if (osInfos.Architecture == System.Runtime.InteropServices.Architecture.X64)
                        {
                            if (await Files.DownloadFileAsync(
                                "https://nightly.link/xfangfang/PPPwn_cpp/workflows/ci.yaml/main/x86_64-windows-gnu.zip",
                                destPath, progress, cancellationToken ?? CancellationToken.None))
                            {
                                return "x86_64-windows-gnu.zip";
                            }
                        }
                        else if (osInfos.Architecture == System.Runtime.InteropServices.Architecture.Arm64)
                        {
                            if (await Files.DownloadFileAsync(
                                "https://nightly.link/xfangfang/PPPwn_cpp/workflows/ci.yaml/main/aarch64-windows-gnu.zip",
                                destPath, progress, cancellationToken ?? CancellationToken.None))
                            {
                                return "aarch64-windows-gnu.zip";
                            }
                        }
                        else
                        {
                            progress?.Report(new DownloadProgress() { Status = DOWNLOAD_PROGRESS_STATUS.FAILED, ErrorMessage = "Architecture not support" });
                        }
                        break;
                    default:
                        progress?.Report(new DownloadProgress() { Status = DOWNLOAD_PROGRESS_STATUS.FAILED, ErrorMessage = "Architecture not support" });
                        break;
                }
            }
            catch (Exception ex)
            {
                progress?.Report(new DownloadProgress() { Status = DOWNLOAD_PROGRESS_STATUS.FAILED, ErrorMessage = ex.ToString() });
            }
            return string.Empty;
        }
        static Task<int> ExecuteAsync(string filePath, bool admin = false)
        {
            return Task.Run(() =>
            {
                try
                {
                    ProcessStartInfo startInfo = new()
                    {
                        FileName = filePath,
                        UseShellExecute = true, // Necessary to use 'runas'
                        CreateNoWindow = true, // Avoids creating a new window
                        WindowStyle = ProcessWindowStyle.Hidden // Hides the window
                    };
                    if(admin)
                    {
                        startInfo.Verb = "runas";  // Requests to run with administrative privileges
                    }
                    Process process = new() { StartInfo = startInfo };
                    process.Start();
                    process.WaitForExit(); // Waits for the process to terminate

                    if (process.ExitCode == 0)
                    {
                        return 0; // All is OK
                    }
                    else
                    {
                        return -1; // Other errors
                    }
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    // A Win32Exception will be thrown if the user cancels the privilege elevation
                    return -2; // Canceled by user
                }
                catch (Exception)
                {
                    return -3; // Error
                }
            });
        }
        static async Task<bool> ExtractStages1(string destPath, Firmware? firmware = null, Progress<ExtractProgress>? progress = null, CancellationToken? cancellationToken = null)
        {
            try
            {
                if (!Directory.Exists(destPath))
                {
                    Directory.CreateDirectory(destPath);
                }
                if (firmware != null)
                {
                    return await Files.ExtractZipEmbeddedResourceAsync("stages1.zip", Path.Combine(firmware.FwWithPoint, "stage1.bin"), destPath, progress);
                }
                else
                {
                    return await Files.ExtractZipEmbeddedResourceAsync("stages1.zip", destPath, progress);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        Task<bool> InstallRequired(CancellationToken? cancellationToken = null)
        {
            bool result = true;
            int step = 0;
            int stepMax = Jobs.Length;
            return Task.Run( async() =>
            {
                try
                {
                    foreach (REQUIRED_JOBS job in Jobs)
                    {
                        // Check if cancellation has been requested before starting the process
                        if (cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested)
                        {
                            cancellationToken.Value.ThrowIfCancellationRequested();
                        }
                        switch (job)
                        {
                            case REQUIRED_JOBS.INSTALL_NPCAP:
                                SetProgress(new RequiredProgress() { Status = REQUIRED_PROGESS_STATUS.DOWNLOADING, CurrentStatus = "Npcap", MainProgressMax = stepMax, MainProgress = step });
                                await Task.Delay(100);
                                result = await DownloadNpcap(Tools.PathTmp, new Progress<DownloadProgress>(SetDownloadProgress));
                                await Task.Delay(100);
                                if (result)
                                {
                                    SetProgress(new RequiredProgress() { Status = REQUIRED_PROGESS_STATUS.INSTALLING, CurrentStatus = "Npcap", MainProgressMax = stepMax, MainProgress = step });
                                    await Task.Delay(100);
                                    int installResult = await ExecuteAsync(Path.Combine(Tools.PathTmp, "npcap-1.79.exe"));
                                    await Task.Delay(100);
                                    switch (installResult)
                                    {
                                        case 0:
                                            break;
                                        case -1:
                                            MessageBox.Show("The install was cancelled.");
                                            result = false;
                                            break;
                                        case -2:
                                            MessageBox.Show("The install failed due to an error.");
                                            result = false;
                                            break;
                                        case -3:
                                            MessageBox.Show("The install failed due to an error.");
                                            result = false;
                                            break;
                                    }
                                }
                                if (File.Exists(Path.Combine(Tools.PathTmp, "npcap-1.60.exe")))
                                {
                                    File.Delete(Path.Combine(Tools.PathTmp, "npcap-1.60.exe"));
                                }
                                break;
                            case REQUIRED_JOBS.INSTALL_PPPWN_CPP:

                                if (!Directory.Exists(Tools.PathTmp))
                                {
                                    Directory.CreateDirectory(Tools.PathTmp);
                                }
                                SetProgress(new RequiredProgress() { Status = REQUIRED_PROGESS_STATUS.DOWNLOADING, CurrentStatus = "PPPwn C++", MainProgressMax = stepMax, MainProgress = step });
                                await Task.Delay(100);
                                string zipFilename = await DownloadPPPwnCPP(Tools.PathTmp, new Progress<DownloadProgress>(SetDownloadProgress));
                                await Task.Delay(100);
                                if (zipFilename != string.Empty)
                                {
                                    SetProgress(new RequiredProgress() { Status = REQUIRED_PROGESS_STATUS.EXTRACTING, CurrentStatus = zipFilename });
                                    await Task.Delay(100);
                                    result = await Files.ExtractZipFileAsync(Path.Combine(Tools.PathTmp, zipFilename), Tools.PathTmp, new Progress<ExtractProgress>(SetExtractProgress));
                                    await Task.Delay(100);
                                    if (result)
                                    {
                                        File.Delete(Path.Combine(Tools.PathTmp, zipFilename));
                                        string destPath = Path.Combine(Tools.PathApp, "C++");
                                        if (!Directory.Exists(destPath))
                                        {
                                            Directory.CreateDirectory(destPath);
                                        }

                                        if (File.Exists(Path.Combine(Tools.PathTmp, "pppwn.exe")))
                                        {
                                            SetProgress(new RequiredProgress() { Status = REQUIRED_PROGESS_STATUS.COPYING, CurrentStatus = "pppwn.exe" });
                                            await Task.Delay(100);
                                            File.Move(Path.Combine(Tools.PathTmp, "pppwn.exe"), Path.Combine(destPath, "pppwn.exe"));
                                            await Task.Delay(100);
                                        }
                                        else if (File.Exists(Path.Combine(Tools.PathTmp, "pppwn.tar.gz")))
                                        {
                                            SetProgress(new RequiredProgress() { Status = REQUIRED_PROGESS_STATUS.EXTRACTING, CurrentStatus = "pppwn.tar.gz" });
                                            await Task.Delay(100);
                                            result = await Files.ExtractTarGzAsync(Path.Combine(Tools.PathTmp, "pppwn.tar.gz"), destPath, new Progress<ExtractProgress>(SetExtractProgress));
                                            await Task.Delay(100);
                                            File.Delete(Path.Combine(Tools.PathTmp, "pppwn.tar.gz"));
                                        }
                                    }
                                }
                                else
                                {
                                    result = false;
                                }
                                if (Directory.Exists(Tools.PathTmp))
                                {
                                    Directory.Delete(Tools.PathTmp, true);
                                }
                                break;
                            case REQUIRED_JOBS.INSTALL_STAGE1:
                                SetProgress(new RequiredProgress() { Status = REQUIRED_PROGESS_STATUS.EXTRACTING, CurrentStatus = $"stage1.bin", MainProgressMax = stepMax, MainProgress = step });
                                await Task.Delay(100);
                                result = await ExtractStages1(Path.Combine(Tools.PathApp, "Stages1"), Tools.MyConfig.Firmware, new Progress<ExtractProgress>(SetExtractProgress));
                                await Task.Delay(100);
                                break;
                            default:
                                break;
                        }
                        step++;
                        if(!result)
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    SetProgress(new RequiredProgress() { Status = REQUIRED_PROGESS_STATUS.FAILED, ErrorMessage = ex.Message });
                    Result = TASK_RESULT.FAILED;
                }
                finally
                {
                    
                }
                //Check if cancellation has been requested before starting the process
                if (cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested)
                {
                    SetProgress(new RequiredProgress() { Status = REQUIRED_PROGESS_STATUS.CANCELED });
                    Result = TASK_RESULT.CANCELED;
                    cancellationToken.Value.ThrowIfCancellationRequested();
                }
                if (result)
                {
                    SetProgress(new RequiredProgress() { Status = REQUIRED_PROGESS_STATUS.COMPLETED, MainProgressMax = stepMax, MainProgress = step });
                    Result = TASK_RESULT.SUCCESSFULL;
                    return false;
                }
                else
                {
                    SetProgress(new RequiredProgress() { Status = REQUIRED_PROGESS_STATUS.FAILED });
                    Result = TASK_RESULT.FAILED;
                    return false;
                }
            });
            
        }
        static bool IsNpcapInstalled()
        {
            try
            {
                string systemDirectory = string.Empty;
                if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess)
                {
                    // Use Sysnative for 32-bit process on 64-bit OS
                    string? sysroot = Environment.GetEnvironmentVariable("SystemRoot");
                    if(sysroot != null)
                    {
                        systemDirectory = Path.Combine(sysroot, "Sysnative");
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    // Use the regular system directory
                    systemDirectory = Environment.SystemDirectory;
                }
                bool result = File.Exists(Path.Combine(systemDirectory, "drivers", "npcap.sys"));
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error IsNpcapInstalled : {ex.Message}");
                return false;
            }
        }
        static bool IsPPPwnCppInstalled()
        {
            return File.Exists(Path.Combine(Environment.CurrentDirectory, "C++", "pppwn.exe"));
        }
        public static REQUIRED_JOBS[]? IsRequiredInstalled(Firmware firmware)
        {
            List<REQUIRED_JOBS> result = [];
            if (!IsNpcapInstalled())
            {
                result.Add(REQUIRED_JOBS.INSTALL_NPCAP);
            }
            if (!IsPPPwnCppInstalled())
            {
                result.Add(REQUIRED_JOBS.INSTALL_PPPWN_CPP);
            }
            if (!IsStage1Installed(firmware))
            {
                result.Add(REQUIRED_JOBS.INSTALL_STAGE1);
            }
            return result.Count > 0 ? [.. result] : null;
        }
        static bool IsStage1Installed(Firmware firmware)
        {
            return File.Exists(Path.Combine(Environment.CurrentDirectory, "Stages1", firmware.FwWithPoint, "stage1.bin"));
        }
        public async void Start()
        {
            await InstallRequired(cancellationToken);
        }
        #endregion
        #region PROGRESS
        void SetDownloadProgress(DownloadProgress downloadProgress)
        {
            if (Callback != null)
            {
                RequiredProgress progress = new();
                if (downloadProgress.ErrorMessage != null)
                {
                    progress.ErrorMessage = downloadProgress.ErrorMessage;
                }
                if (downloadProgress.Filename != null)
                {
                    switch (downloadProgress.Status)
                    {
                        case DOWNLOAD_PROGRESS_STATUS.COMPLETED:
                            progress.CurrentStatus = $"Download {downloadProgress.Filename} successfull.";
                            progress.CurrentProgress = 100 ;
                            progress.CurrentProgressMax = 100 ;
                            progress.ConsoleMessage = progress.CurrentStatus;
                            break;
                        case DOWNLOAD_PROGRESS_STATUS.DOWNLOADING:
                            progress.CurrentStatus = $"Downloading {downloadProgress.Filename} {Files.Round((decimal)downloadProgress.Percentage, 2)}%.";
                            progress.ConsoleMessage = progress.CurrentStatus;
                            break;
                        case DOWNLOAD_PROGRESS_STATUS.FAILED:
                        case DOWNLOAD_PROGRESS_STATUS.CANCELED:
                        case DOWNLOAD_PROGRESS_STATUS.STARTING:
                            progress.CurrentStatus = $"{downloadProgress.Status.Description().ToFirstUpper()} download {downloadProgress.Filename}.";
                            progress.ConsoleMessage = progress.CurrentStatus;
                            break;
                        case DOWNLOAD_PROGRESS_STATUS.NONE:

                            break;

                    }
                }
                progress.CurrentProgress = downloadProgress.BytesRead;
                progress.CurrentProgressMax = downloadProgress.TotalBytes;
                Callback.Report(progress);
            }
        }
        void SetExtractProgress(ExtractProgress extractProgress)
        {
            if (Callback != null)
            {
                RequiredProgress progress = new();
                if (extractProgress.ErrorMessage != null)
                {
                    progress.ErrorMessage = extractProgress.ErrorMessage;
                }
                if (extractProgress.Filename != null)
                {
                    switch (extractProgress.Status)
                    {
                        case EXTRACT_PROGRESS_STATUS.COMPLETED:
                            progress.CurrentStatus = $"Extract {extractProgress.Filename} successfull.";
                            progress.ConsoleMessage = progress.CurrentStatus;
                            progress.CurrentProgress = 100;
                            progress.CurrentProgressMax = 100;
                            break;
                        case EXTRACT_PROGRESS_STATUS.EXTRACTING:
                            progress.CurrentStatus = $"Extracting {extractProgress.Filename} {Files.Round((decimal)extractProgress.BytesPercentage, 2)}%.";
                            progress.ConsoleMessage = progress.CurrentStatus;
                            break;
                        case EXTRACT_PROGRESS_STATUS.FAILED:
                        case EXTRACT_PROGRESS_STATUS.CANCELED:
                            progress.CurrentStatus = $"{extractProgress.Status.Description().ToFirstUpper()} extract {extractProgress.Filename}.";
                            progress.ConsoleMessage = progress.CurrentStatus;
                            break;
                        case EXTRACT_PROGRESS_STATUS.NONE:

                            break;
                    }
                }
                if(extractProgress.Status != EXTRACT_PROGRESS_STATUS.COMPLETED)
                {
                    progress.CurrentProgress = extractProgress.BytesRead;
                    progress.CurrentProgressMax = extractProgress.TotalBytes;
                }
                Callback.Report(progress);
            }
        }
        void SetProgress(RequiredProgress progress)
        {
            if(Callback != null && progress.Status != REQUIRED_PROGESS_STATUS.NONE)
            {
                if (progress.Status == REQUIRED_PROGESS_STATUS.STARTING)
                {
                    progress.CurrentStatus = $"{progress.Status.Description().ToFirstUpper()}";
                    progress.ConsoleMessage = progress.CurrentStatus;
                }
                else if(progress.Status != REQUIRED_PROGESS_STATUS.COMPLETED)
                {
                    progress.CurrentStatus = $"{progress.Status.Description().ToFirstUpper()} {progress.CurrentStatus} ...";
                    progress.ConsoleMessage = progress.CurrentStatus;
                }
                Callback.Report(progress);
            }
        }
        #endregion
    }
}
