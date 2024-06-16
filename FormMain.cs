using DpLib.Extensions;
using DpLib.Helpers;
using DpLib.Models;
using DpLib.Winform;
using DpLib.Winform.Controls;
using EzPPPwn.Enums;
using EzPPPwn.Helpers;
using EzPPPwn.Models;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace EzPPPwn
{
    public partial class FormMain : Form
    {
        int originalFormHeight;
        int originalTextBoxHeight;
        ReleaseInfos latestInfos = new();
        PPPwnTask? worker;
        private readonly Timer timer = new();
        TimeSpan timeElapsed;
        public FormMain()
        {
            InitializeComponent();
            Text = $"Ez PPPwn v{Tools.GetVersionStr()}";
            originalFormHeight = this.Height;
            originalTextBoxHeight = textBoxLog.Height;
        }
        #region EVENTS
        private async void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (timer != null)
                {

                    timer.Stop();
                    timer.Tick -= TimerTick;
                }
                worker?.Cancel();
                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cancel : Error ButtonCancel_Click :\n{ex.Message}");
            }
            finally
            {

            }
        }
        private async void FormMain_Shown(object sender, EventArgs e)
        {
            buttonCancel.Visible = false;
            buttonStart.Visible = false;
            await Init();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (timer != null)
                {
                    timer.Stop();
                    timer.Tick -= TimerTick;
                }
                LockButtons(false);
                worker?.Cancel();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cancel : Error ButtonCancel_Click :\n{ex.Message}");
            }
            finally
            {
                LockButtons(false);
            }
        }
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            RunExploit();
        }
        private void ShowConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.MyConfig.ShowConsole = !Tools.MyConfig.ShowConsole;
            ShowConsole();
            Tools.MyConfig.Save();
        }
        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenConfig();
        }
        private void PictureBoxGitHub_Click(object sender, EventArgs e)
        {
            OpenGitHub();
        }
        private void TimerTick(object? sender, EventArgs e)
        {
            timeElapsed = timeElapsed.Add(TimeSpan.FromSeconds(1));
            string seconds;
            if (timeElapsed.Seconds.ToString().Length.Equals(1))
            {
                seconds = "0" + timeElapsed.Seconds.ToString();
            }
            else
            {
                seconds = timeElapsed.Seconds.ToString();
            }
            string minutes;
            if (timeElapsed.Minutes.ToString().Length.Equals(1))
            {
                minutes = "0" + timeElapsed.Minutes.ToString();
            }
            else
            {
                minutes = timeElapsed.Minutes.ToString();
            }
            labelTimer.Text = $"{minutes}:{seconds}";
        }
        void UpdateCppPPPwnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCpp();
        }

        void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUpdate();
        }
        #endregion
        #region FUNCTIONS
        void AdjustFormAndControls(bool showTextBox)
        {
            if (showTextBox)
            {
                // Show the TextBox, reset controls to original positions
                pictureBoxGitHub.Top = textBoxLog.Bottom + 10; // Adjust the controls' top position
                labelDjPopol.Top = textBoxLog.Bottom + 10;
                Height = originalFormHeight;
            }
            else
            {
                // Hide the TextBox, move controls up and shrink form height
                pictureBoxGitHub.Top = textBoxLog.Top; // Adjust the controls' top position
                labelDjPopol.Top = textBoxLog.Top;
                Height = originalFormHeight - originalTextBoxHeight;
            }
        }
        async Task Init()
        {
            Refresh();
            UpdateManager updateManager = new("https://api.github.com/repos/DjPopol/EzPPPwn/releases", Tools.GetToken());
            Version currentVersion = Tools.GetVersion();
            if(await Tools.IsConnectedToInternetAsync())
            {
                latestInfos = await updateManager.GetLastReleaseInfosAsync();
            }
            else
            {
                latestInfos = new();
            }
            bool IsUptoDate = !await Tools.IsConnectedToInternetAsync() || latestInfos.Version == new Version() || latestInfos.Version <= currentVersion;
            updateCppToolStripMenuItem.Visible = !IsUptoDate;
            if (Tools.MyConfig.CheckUpdateOnStartUp && !IsUptoDate)
            {
                ShowUpdate();
            }
            REQUIRED_JOBS[]? requiredJobs = RequiredTask.IsRequiredInstalled(Tools.MyConfig.Firmware);

            if (Tools.MyConfig == null || !Tools.MyConfig.CheckConfig())
            {
                OpenConfig();
            }
            else if (requiredJobs != null && requiredJobs.Length > 0)
            {
                if ((requiredJobs.Contains(REQUIRED_JOBS.INSTALL_NPCAP) || requiredJobs.Contains(REQUIRED_JOBS.INSTALL_PPPWN_CPP)) && !await Tools.IsConnectedToInternetAsync())
                {
                    MessageBox.Show("Internet connexion  required !\nYou must connect to internet and restart application", "Install requirements", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                }
                else
                {
                    string message = $"You must install this requirement{(requiredJobs.Length > 1 ? "s" : "")} :";
                    foreach (REQUIRED_JOBS job in requiredJobs)
                    {
                        message += $"\n- {job.Description().Replace("INSTALL_", "").ToFirstUppers('_').Replace("_", " ")}";
                        if (job == REQUIRED_JOBS.INSTALL_STAGE1)
                        {
                            message += $" {Tools.MyConfig.Firmware.FwWithPoint}";
                        }
                    }
                    message += $"\nWould you like to install {(requiredJobs.Length > 1 ? "them" : "this")}? ";
                    DialogResult result = MessageBox.Show(message, "Install requirements", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Install Requirements
                        FormInstallRequired formInstallRequired = new(requiredJobs);
                        formInstallRequired.FormClosing += new FormClosingEventHandler(async (object? sender, FormClosingEventArgs e) =>
                        {
                            Enabled = true;
                            await Init();
                        });
                        Enabled = false;
                        await Task.Delay(100);
                        formInstallRequired.Show();
                        Hide();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
            else if (Tools.MyConfig == null || !Tools.MyConfig.CheckConfig())
            {
                OpenConfig();
            }
            else
            {
                buttonStart.Visible = true;
                ShowConsole();
                buttonStart.Focus();
                labelStatus.Text = "Ready ?";
                labelFirmware.Text = Tools.MyConfig.Firmware.FwWithPoint;
                updateCppToolStripMenuItem.Visible = await Tools.IsConnectedToInternetAsync();
                Show();
            }
            return;
        }
        void LockButtons(bool locked)
        {
            buttonCancel.Visible = locked;
            buttonStart.Visible = !locked;
            toolStripMenuItemConfig.Enabled = !locked;
            updateCppToolStripMenuItem.Enabled = !locked;
        }
        void OpenConfig()
        {
            FormConfig formConfig = new();
            formConfig.FormClosing += new FormClosingEventHandler(async (object? sender, FormClosingEventArgs e) =>
            {
                bool checkConfig = Tools.MyConfig.CheckConfig();
                if (!checkConfig)
                {
                    Close();
                }
                else
                {
                    Enabled = true;
                    await Init();
                }
            });
            Hide();
            Enabled = false;
            formConfig.Show();
        }
        void OpenGitHub()
        {
            try
            {
                ProcessStartInfo psi = new()
                {
                    FileName = "https://github.com/DjPopol/EzPPPwn",
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening page: " + ex.Message);
            }
        }
        void RunExploit()
        {
            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "C++", "pppwn.exe")))
            {
                MessageBox.Show($"pppwn.exe not found in {Path.Combine(Environment.CurrentDirectory, "C++", "pppwn.exe")}", "pppwn.exe", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!Tools.MyConfig.CheckConfig())
            {
                MessageBox.Show($"Config not completed\nPlease complete", "Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LockButtons(true);
                try
                {
                    timeElapsed = new(0);
                    timer.Interval = 1000;
                    timer.Tick += new EventHandler(TimerTick);
                    worker = new(new Progress<PPPwnProgress>(SetProgress));
                    timer.Start();
                    worker.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        void SetProgress(PPPwnProgress progress)
        {
            Invoke(new Action(() =>
            {
                if (progress.Message != string.Empty)
                {
                    progressBar.CustomText = progress.Message;
                }
                if (progress.ConsoleMessage != string.Empty)
                {
                    textBoxLog.AppendText(progress.ConsoleMessage);
                    labelStatus.Text = progress.ConsoleMessage;
                }
                if (progress.ProgressMax != null)
                {
                    progressBar.Maximum = (int)Math.Round(progress.ProgressMax.Value, 0);
                }
                if (progress.Progress != null)
                {
                    progressBar.Value = (int)Math.Round(progress.Progress.Value, 0);
                }

                if (progress.Status == PPPWN_PROGESS_STATUS.COMPLETED || progress.Status == PPPWN_PROGESS_STATUS.CANCELED || progress.Status == PPPWN_PROGESS_STATUS.ERROR || progress.Status == PPPWN_PROGESS_STATUS.FAILED)
                {

                    if (progress.Status == PPPWN_PROGESS_STATUS.FAILED && Tools.MyConfig.PPPwnOptions.UseAutoRetry)
                    {

                    }
                    else
                    {
                        timer.Stop();
                        timer.Tick -= TimerTick;
                        LockButtons(false);
                    }
                }
            }));
        }
        void ShowConsole()
        {
            //Height = Tools.MyConfig.ShowConsole ? 355 : 170;
            showConsoleToolStripMenuItem.Text = Tools.MyConfig.ShowConsole ? "Hide Console" : "Show Console";
            if (textBoxLog.Visible)
            {
                // Hide the TextBox
                textBoxLog.Visible = false;
                AdjustFormAndControls(false);
            }
            else
            {
                // Show the TextBox
                textBoxLog.Visible = true;
                AdjustFormAndControls(true);
            }
        }
        async void ShowUpdate()
        {
            if (await Tools.IsConnectedToInternetAsync())
            {
                DpMessageBox messageBox = new($"{latestInfos.Name} is avaible.\nWould you like to update ?", "New Update avaible", MessageBoxButtons.YesNo, MessageBoxIcon.Question, true, Tools.MyConfig.CheckUpdateOnStartUp)
                {
                    CheckBoxText = "Show at startup"
                };
                DialogResult = messageBox.ShowDialog();
                Tools.MyConfig.CheckUpdateOnStartUp = messageBox.CheckBoxChecked;
                Tools.MyConfig.Save();
                if (DialogResult == DialogResult.Yes)
                {
                    // Update
                    DpFormUpdate formUpdate = new(latestInfos, Tools.MyConfig.ShowConsole);
                    formUpdate.FormClosing += new FormClosingEventHandler((object? sender, FormClosingEventArgs e) =>
                    {
                        Enabled = true;
                        Close();
                    });
                    Enabled = false;
                    await Task.Delay(100);
                    formUpdate.Show();
                    Hide();
                }
            }
            else
            {
                MessageBox.Show("Internet connexion  required !\nYou must connect to internet and restart application", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        async void UpdateCpp()
        {
            if (await Tools.IsConnectedToInternetAsync())
            {
                FormInstallRequired formInstallRequired = new([REQUIRED_JOBS.INSTALL_PPPWN_CPP]);
                formInstallRequired.FormClosing += new FormClosingEventHandler((object? sender, FormClosingEventArgs e) =>
                {
                    Enabled = true;
                    Show();
                });
                Hide();
                Enabled = false;
                formInstallRequired.Show();
            }
            else
            {
                MessageBox.Show("Internet connexion  required !\nYou must connect to internet and restart application", "Update PPPwn C++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        #endregion
    }
}
