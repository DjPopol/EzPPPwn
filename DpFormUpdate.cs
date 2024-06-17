using DpLib.Enums;
using DpLib.Extensions;
using DpLib.Helpers;
using DpLib.Models;
using DpLib.Winform.Controls;
using System.Diagnostics;

namespace DpLib.Winform
{
    public partial class DpFormUpdate : Form
    {
        static string pathTmp = string.Empty;
        public TASK_RESULT Result = TASK_RESULT.NONE;
        readonly ReleaseInfos _releaseInfos = new ();
        readonly CancellationToken cancellationToken = new();
        bool _showConsole;
        public DpFormUpdate(ReleaseInfos releaseInfos, bool showConsole)
        {
            pathTmp = Path.Combine(Path.GetTempPath(), "EzPPPwn");
            _releaseInfos = releaseInfos;
            InitializeComponent();
            Text = $"Update {releaseInfos.Name}";
            _showConsole = showConsole;
        }

        #region EVENTS
        async void DpFormUpdate_Shown(object sender, EventArgs e)
        {
            await StartUpdate();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cancel : Error ButtonCancel_Click :\n{ex.Message}");
            }
            finally
            {

            }
        }
        private void ShowConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _showConsole = !_showConsole;
            ShowConsole();
        }
        #endregion
        #region FUNCTIONS
        public void EndUpdate()
        {
            string batchScriptPath = Path.Combine(pathTmp, "update.bat");
            // Execute the batch script
            ProcessStartInfo psi = new()
            {
                FileName = batchScriptPath,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                UseShellExecute = true,
            };
            Process process = new() { StartInfo = psi };
            process.Start();
        }
        private void ShowConsole()
        {
            textBoxConsole.Top = buttonCancel.Visible ? 147 : 121;
            Height = _showConsole ? buttonCancel.Visible ? 340 : 315 : buttonCancel.Visible ? 185 : 160;
            showConsoleToolStripMenuItem.Text = _showConsole ? "Hide Console" : "Show Console";
        }
        public async Task<bool> StartUpdate()
        {
            bool result = true;
            int stepMax = 4; // Update/Extract/Create update.bat/Execute update.bat
            int step = 0;

            try
            {
                // Step 1: Ensure the temporary directory exists
                if (Directory.Exists(pathTmp))
                {
                    Directory.Delete(pathTmp, true);
                }
                Directory.CreateDirectory(pathTmp);
                // Report progress: downloading
                step++;
                SetProgress(new UpdateProgress() { Status = UPDATE_PROGESS_STATUS.DOWNLOADING, CurrentStatus = _releaseInfos.Name, MainProgressMax = stepMax, MainProgress = step });

                // Simulate download delay
                await Task.Delay(100);

                // Step 2: Download the file
                bool downloadResult = await Files.DownloadFileAsync(_releaseInfos.DownloadURL, pathTmp, new Progress<DownloadProgress>(SetDownloadProgress), cancellationToken);

                // Simulate delay
                await Task.Delay(100);

                if (downloadResult)
                {
                    step++;
                    string zipFilename = Files.GetFileNameFromUrl(_releaseInfos.DownloadURL);

                    // Report progress: extracting
                    SetProgress(new UpdateProgress() { Status = UPDATE_PROGESS_STATUS.EXTRACTING, CurrentStatus = zipFilename, MainProgress = step });

                    // Simulate extraction delay
                    await Task.Delay(100);
                    string outPath = Path.Combine(pathTmp, "Update");
                    if (!Directory.Exists(outPath))
                    {
                        Directory.CreateDirectory(outPath);
                    }
                    // Step 3: Extract the downloaded file
                    result = await Files.ExtractZipFileAsync(Path.Combine(pathTmp, zipFilename), outPath, new Progress<ExtractProgress>(SetExtractProgress));

                    // Simulate delay
                    await Task.Delay(100);
                    File.Delete(Path.Combine(pathTmp, zipFilename));
                    if (result)
                    {
                        step++;
                        SetProgress(new UpdateProgress() { Status = UPDATE_PROGESS_STATUS.INSTALLING, CurrentStatus = zipFilename, MainProgress = step });
                        // Step 4: Check if the .exe file exists in the temp folder
                        string[] exeFiles = Directory.GetFiles(outPath, "*.exe");
                        if (exeFiles.Length == 1)
                        {
                            string pathApp = Environment.CurrentDirectory;
                            string batchScriptPath = Path.Combine(pathTmp, "update.bat");
                            string newAppPath = exeFiles[0].Replace(outPath,pathApp); // Assuming there's only one .exe file
                            // Step 5: Generate the batch script
                            // Step 5-1: Kill the current application process
                            Process currentProcess = Process.GetCurrentProcess();
                            int currentProcessId = currentProcess.Id;
                            string killCommand = $"taskkill /PID {currentProcessId} /F";
                            // Step 5-2: Move files from temporary folder to current directory
                            string moveFilesCommand = $"xcopy \"{outPath}\" \"{pathApp}\"   /E /Y /C /H";
                            // Step 5-3: Clean temporary folder
                            string deleteTmpFolderCommand = $"rmdir /S /Q \"{pathTmp}\"";
                            // Step 5-4: Start the new version of the application
                            string startNewAppCommand = $"\"{newAppPath}\"";
                            // Step 5-5: Write the script
                            using StreamWriter sw = new(batchScriptPath);
                            
                            await sw.WriteLineAsync(killCommand);
                            await sw.WriteLineAsync(moveFilesCommand);
                            await sw.WriteLineAsync(startNewAppCommand);
                            await sw.WriteLineAsync(deleteTmpFolderCommand);
                        }
                        else
                        {
                            Console.WriteLine("Error: Unable to find or identify the executable in the temp folder.");
                            result = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Failed to extract the downloaded file.");
                        result = false;
                    }
                }
                else
                {
                    Console.WriteLine("Error: Failed to download the update.");
                    result = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating application: {ex.Message}");
                result = false;
            }
            finally
            {
                // Cleanup temporary directory
                if (Directory.Exists(pathTmp))
                {
                    //Directory.Delete(Tools.PathTmp, true);
                }

                // Report final progress and result
                if (cancellationToken.IsCancellationRequested)
                {
                    SetProgress(new UpdateProgress() { Status = UPDATE_PROGESS_STATUS.CANCELED });
                    Result = TASK_RESULT.CANCELED;
                    cancellationToken.ThrowIfCancellationRequested();
                }
                else if (result)
                {
                    SetProgress(new UpdateProgress() { Status = UPDATE_PROGESS_STATUS.COMPLETED, MainProgressMax = stepMax, MainProgress = step });
                    Result = TASK_RESULT.SUCCESSFULL;
                }
                else
                {
                    SetProgress(new UpdateProgress() { Status = UPDATE_PROGESS_STATUS.FAILED });
                    Result = TASK_RESULT.FAILED;
                }
            }
            return result;
        }
        #endregion
        #region Progress
        void SetDownloadProgress(DownloadProgress downloadProgress)
        {
            UpdateProgress progress = new();
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
                        progress.CurrentProgress = 100;
                        progress.CurrentProgressMax = 100;
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
            SetProgress(progress);
        }
        void SetExtractProgress(ExtractProgress extractProgress)
        {
            UpdateProgress progress = new();
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
            if (extractProgress.Status != EXTRACT_PROGRESS_STATUS.COMPLETED)
            {
                progress.CurrentProgress = extractProgress.BytesRead;
                progress.CurrentProgressMax = extractProgress.TotalBytes;
            }
            SetProgress(progress);
        }
        public void SetProgress(UpdateProgress progress)
        {
            Invoke(new Action(() =>
            {
                if (progress.ConsoleMessage != string.Empty)
                {
                    textBoxConsole.AppendText(progress.ConsoleMessage + Environment.NewLine);
                }
                if (progress.CurrentStatus != string.Empty)
                {
                    labelCurrentStatus.Text = progress.CurrentStatus;
                }
                if (progress.CurrentProgressStatus != string.Empty)
                {
                    progressBarCurrent.CustomText = progress.CurrentProgressStatus;
                }

                if (progress.CurrentProgressMax != null)
                {
                    progressBarCurrent.Maximum = (int)progress.CurrentProgressMax.Value;
                }
                if (progress.CurrentProgress != null)
                {
                    int cur = (int)progress.CurrentProgress.Value;
                    progressBarCurrent.Value = cur <= progress.CurrentProgressMax ? cur : progress.CurrentProgressMax != null ? (int)progress.CurrentProgressMax : 0;
                }
                if (progress.ErrorMessage != string.Empty)
                {
                    DpMessageBox.ShowDialog(progress.ErrorMessage, "Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                progressBarMain.CustomText = $"Updating...";
                if (progress.MainProgressMax != null)
                {
                    progressBarMain.Maximum = progress.MainProgressMax.Value;
                }
                if (progress.MainProgress != null)
                {
                    progressBarMain.Value = progress.MainProgress.Value;
                }
                if (progress.Status == UPDATE_PROGESS_STATUS.CANCELED || progress.Status == UPDATE_PROGESS_STATUS.FAILED || progress.Status == UPDATE_PROGESS_STATUS.COMPLETED)
                {
                    progressBarMain.CustomText = $"Update";
                    buttonCancel.Visible = false;
                    if (progress.Status == UPDATE_PROGESS_STATUS.CANCELED)
                    {
                        DpMessageBox.ShowDialog("Update canceled.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (progress.Status == UPDATE_PROGESS_STATUS.FAILED)
                    {
                        DpMessageBox.ShowDialog("Update failed.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (progress.Status == UPDATE_PROGESS_STATUS.COMPLETED)
                    {
                        DialogResult = DpMessageBox.ShowDialog("Update successfull.\nThe application will restart to complete the update.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        EndUpdate();
                    }
                    Close();
                }
            }));
        }
        #endregion
    }
}
