using System.Diagnostics;

namespace Ez_PPPwn
{
    public partial class FormConsole : Form
    {
        private readonly ExecuteInfos _infos;

        public Thread? th;
        public FormConsole(ExecuteInfos infos)
        {
            _infos = infos;
            InitializeComponent();
        }
        #region EVENTS
        private void FormConsole_Load(object sender, EventArgs e)
        {
            Execute(_infos);
        }
        private void ButtonSaveLog_Click(object sender, EventArgs e)
        {
            SaveLog();
        }
        private void Cmd_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data) && textBoxLog != null)
            {
                try
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            textBoxLog.AppendText(e.Data + Environment.NewLine);
                        }));
                    }
                    else
                    {
                        textBoxLog.AppendText(e.Data + Environment.NewLine);
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cmd_DataReceived : {ex}");
                }
            }
        }
        #endregion
        #region FUNCTIONS
        private void Execute(ExecuteInfos infos)
        {
            if(infos.PathToScript == null || !File.Exists(infos.PathToScript) ||
                infos.PathToOffsets == null || !File.Exists(infos.PathToOffsets) ||
                infos.Stage1Path == null || !File.Exists(infos.Stage1Path) || infos.Stage2Path == null || !File.Exists(infos.Stage2Path))
            {
                Close();
            }
            else
            {
                textBoxLog.Text = string.Empty;
                try
                {

                    th = new(async () =>
                    {
                        string pppwnPathOut = infos.PathToScript;
                        if (Path.GetDirectoryName(infos.PathToScript) != Path.GetDirectoryName(infos.PathToOffsets))
                        {
                            if (Directory.Exists(Tools.PathTmp))
                            {
                                Directory.Delete(Tools.PathTmp, true);
                            }
                            Directory.CreateDirectory(Tools.PathTmp);
                            await Tools.SavePythonScripts(Path.Combine(Tools.PathTmp, Path.GetFileName(infos.PathToScript)), infos.PathToScript, infos.PathToOffsets, infos.Firmware);
                            pppwnPathOut = Path.Combine(Tools.PathTmp, Path.GetFileName(infos.PathToScript));
                        }

                        Process process = new()
                        {
                            StartInfo = new ProcessStartInfo()
                            {
                                WindowStyle = ProcessWindowStyle.Normal,
                                FileName = "python.exe",
                                Arguments = $" \"{pppwnPathOut}\" --interface=\"{infos.NetworkInterface}\" --fw={infos.Firmware.Replace(".", "")} --stage1=\"{infos.Stage1Path}\" --stage2=\"{infos.Stage2Path}\"",
                                UseShellExecute = false,
                                CreateNoWindow = true,
                                RedirectStandardOutput = true,
                                RedirectStandardError = true,
                            },
                            EnableRaisingEvents = true,
                        };
                        try
                        {
                           
                            process.OutputDataReceived += Cmd_DataReceived;
                            process.ErrorDataReceived += Cmd_DataReceived;
                            process.Start();
                            process.BeginOutputReadLine();
                            process.BeginErrorReadLine();
                            process.WaitForExit();
                            process.Close();
                        }
                        finally
                        {
                            if (InvokeRequired)
                            {
                                Invoke(new Action(async () =>
                                {
                                    buttonSaveLog.Enabled = true;
                                    await Task.Delay(1000);
                                }));
                            }
                            else
                            {
                                buttonSaveLog.Enabled = true;
                            };
                        }
                    }); 
                    th.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private void SaveLog()
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "log files (*.log)|*.log"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new(saveFileDialog.FileName);
                sw.WriteLine(textBoxLog.Text);
                sw.Close();
            }
        }
        #endregion
    }
}
