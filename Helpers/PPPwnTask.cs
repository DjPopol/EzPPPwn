using EzPPPwn.Enums;
using EzPPPwn.Models;
using System.Diagnostics;

namespace EzPPPwn.Helpers
{
    public class PPPwnTask(IProgress<PPPwnProgress>? callback, CancellationToken? cancellationToken = null )
    {
        TASK_RESULT Result = TASK_RESULT.NONE;
        public Process process = new();
        async void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                try
                {
                    if (e.Data.Contains("STAGE 0", StringComparison.CurrentCultureIgnoreCase))
                    {
                        callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.STAGE0, ConsoleMessage = e.Data + Environment.NewLine, Message = e.Data, Progress = 0.0 });
                    }
                    else if (e.Data.Contains("STAGE 1", StringComparison.CurrentCultureIgnoreCase))
                    {
                        callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.STAGE1, ConsoleMessage = e.Data + Environment.NewLine, Message = e.Data, Progress = 20.0 });
                    }
                    else if (e.Data.Contains("STAGE 2", StringComparison.CurrentCultureIgnoreCase))
                    {
                        callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.STAGE2, ConsoleMessage = e.Data + Environment.NewLine, Message = e.Data, Progress = 40.0 });
                    }
                    else if (e.Data.Contains("STAGE 3", StringComparison.CurrentCultureIgnoreCase))
                    {
                        callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.STAGE3, ConsoleMessage = e.Data + Environment.NewLine, Message = e.Data, Progress = 60.0 });
                    }
                    else if (e.Data.Contains("STAGE 4", StringComparison.CurrentCultureIgnoreCase))
                    {
                        callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.STAGE4, ConsoleMessage = e.Data + Environment.NewLine, Message = e.Data , Progress = 80.0 });
                    }
                    else if (e.Data.Contains("Done!", StringComparison.CurrentCultureIgnoreCase))
                    {
                        callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.COMPLETED, ConsoleMessage = e.Data + Environment.NewLine, Message = e.Data , Progress = 100.0 });
                        Result = TASK_RESULT.SUCCESSFULL;
                    }
                    else if (e.Data.Contains("Failed", StringComparison.CurrentCultureIgnoreCase))
                    {
                        callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.FAILED, ConsoleMessage = e.Data + Environment.NewLine, Message = e.Data });
                        if (!Tools.MyConfig.PPPwnConfig.UseAutoRetry && !process.HasExited)
                        {
                            process.Kill();
                        }
                        Result = TASK_RESULT.FAILED;
                    }
                    else if (e.Data.Contains("Error", StringComparison.CurrentCultureIgnoreCase))
                    {
                        callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.ERROR, ConsoleMessage = e.Data + Environment.NewLine,Message = e.Data });
                        if (!process.HasExited)
                        {
                            process.Kill();
                        }
                        Result = TASK_RESULT.ERROR;
                    }
                    else
                    {
                        callback?.Report(new PPPwnProgress() { ConsoleMessage = e.Data + Environment.NewLine });
                    }
                    await Task.Delay(100);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Cmd_DataReceived : {ex}");
                    callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.ERROR, Message = ex.Message });
                    return;
                }
            }
        }
        public void Cancel()
        {
            callback = null;
            if (process != null && !process.HasExited)
            {
                process.Kill(true);
            }
            Tools.StopAllProcessesByName("pppwn.exe");
            
            Result = TASK_RESULT.CANCELED;
        }
        Task<bool> RunPPPwn()
        {
            return Task.Run(() =>
            {
                // Check if cancellation has been requested before starting the process
                if (cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested)
                {
                    cancellationToken.Value.ThrowIfCancellationRequested();
                }
                Tools.StopAllProcessesByName("pppwn.exe");
                if (Tools.MyConfig != null && Tools.MyConfig.NetworkInterface != null)
                {
                    string stage1Path = Path.Combine(Environment.CurrentDirectory, "Stages1", Tools.MyConfig.Firmware.FwWithPoint, "stage1.bin");
                    callback?.Report(new PPPwnProgress()
                        {
                            Status = PPPWN_PROGESS_STATUS.STARTING,
                            Message = "Starting PPPwn...",
                            Progress = 0.0,
                            ProgressMax = 100.0
                        }
                    );
                    string args = $" --interface \"\\Device\\NPF_{Tools.MyConfig.NetworkInterface.Id}\" --fw {Tools.MyConfig.Firmware.Fw} --stage1 \"{stage1Path}\" --stage2 \"{Tools.MyConfig.Stage2Path}\"";
                    if (Tools.MyConfig.PPPwnConfig.UseTimeOut)
                    {
                        args += $" --timeout {Tools.MyConfig.PPPwnConfig.TimeOut}";
                    }
                    if (Tools.MyConfig.PPPwnConfig.UseWaitAfterPIN)
                    {
                        args += $" --wait-after-pin {Tools.MyConfig.PPPwnConfig.WaitAfterPIN}";
                    }
                    if (Tools.MyConfig.PPPwnConfig.UseGroomDelay)
                    {
                        args += $" --groom-delay {Tools.MyConfig.PPPwnConfig.GroomDelay}";
                    }
                    if (Tools.MyConfig.PPPwnConfig.UseAutoRetry)
                    {
                        args += $" --auto-retry";
                    }
                    if (Tools.MyConfig.PPPwnConfig.UseNoWaitPADI)
                    {
                        args += " --no-wait-padi";
                    }
                    if (Tools.MyConfig.PPPwnConfig.UseRealSleep)
                    {
                        args += " --real-sleep";
                    }
                    process = new()
                    {
                        StartInfo = new ProcessStartInfo()
                        {
                            WindowStyle = ProcessWindowStyle.Normal,
                            FileName = Path.Combine(Environment.CurrentDirectory, "C++", "pppwn.exe"),
                            Arguments = args,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                        },
                        EnableRaisingEvents = true,
                    };
                    try
                    {
                        process.OutputDataReceived += OutputDataReceived;
                        process.ErrorDataReceived += OutputDataReceived;
                        process.Start();
                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();
                        process.WaitForExit();
                    }
                    finally
                    {
                        
                    };
                    if (cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested || Result == TASK_RESULT.CANCELED)
                    {
                        callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.CANCELED, Message = "Canceled" });
                    }
                    else if (Result == TASK_RESULT.ERROR || process.ExitCode != 0)
                    {
                        callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.ERROR, Message = "Error" });
                    }
                    else if (Result == TASK_RESULT.FAILED)
                    {
                        callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.FAILED, Message = "PPPwn Failed", Progress = 100.0, Result = Result });
                    }
                    else if (Result == TASK_RESULT.SUCCESSFULL)
                    {
                        callback?.Report(new PPPwnProgress() { Status = PPPWN_PROGESS_STATUS.COMPLETED, Message = "PPPwn Completed", Progress = 100.0, Result = Result });
                    }
                }
                return true;
            });
        }
        public async void Start()
        {
            await RunPPPwn();   
        }
    }
}
