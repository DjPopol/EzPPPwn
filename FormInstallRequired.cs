using DpLib.Winform.Controls;
using EzPPPwn.Enums;
using EzPPPwn.Helpers;
using EzPPPwn.Models;

namespace EzPPPwn
{
    public partial class FormInstallRequired : Form
    {
        public REQUIRED_JOBS[] _jobs;
        RequiredTask? task;
        readonly CancellationToken cancellationToken = new ();
        public FormInstallRequired(REQUIRED_JOBS[] jobs)
        {
            _jobs = jobs;
            InitializeComponent();
            Text = $"Ez PPPwn v{Tools.GetVersionStr()}";
        }
        #region EVENTS
        private void FormCheckRequired_Shown(object sender, EventArgs e)
        {
            ShowConsole();
            task = new(_jobs, new Progress<RequiredProgress>(SetProgress), cancellationToken);
            task.Start();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested ();
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
            Tools.MyConfig.ShowConsole = !Tools.MyConfig.ShowConsole;
            ShowConsole();
            Tools.MyConfig.Save();
        }
        #endregion
        #region FUNCTIONS
        public void SetProgress(RequiredProgress progress)
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
                    DpMessageBox.ShowDialog(progress.ErrorMessage, "Install required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                progressBarMain.CustomText = $"Install requirement{(progress.MainProgress > 1 ? "s" : "")}";
                if (progress.MainProgressMax != null)
                {
                    progressBarMain.Maximum = progress.MainProgressMax.Value;
                }
                if (progress.MainProgress != null)
                {
                    progressBarMain.Value = progress.MainProgress.Value;
                }
                if (progress.Status == REQUIRED_PROGESS_STATUS.CANCELED || progress.Status == REQUIRED_PROGESS_STATUS.FAILED || progress.Status == REQUIRED_PROGESS_STATUS.COMPLETED)
                {
                    buttonCancel.Visible = false;
                    ShowConsole();
                    if (progress.Status == REQUIRED_PROGESS_STATUS.CANCELED)
                    {
                        DpMessageBox.ShowDialog("Install canceled.", "Install required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (progress.Status == REQUIRED_PROGESS_STATUS.FAILED)
                    {
                        DpMessageBox.ShowDialog("Install failed.", "Install required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (progress.Status == REQUIRED_PROGESS_STATUS.COMPLETED)
                    {
                        DpMessageBox.ShowDialog("Install successfull.", "Install required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Close();
                }
            }));
        }
        private void ShowConsole()
        {
            textBoxConsole.Top = buttonCancel.Visible ? 147 : 121;
            Height = Tools.MyConfig.ShowConsole ? buttonCancel.Visible ? 340 : 315 : buttonCancel.Visible ? 185 : 160;
            showConsoleToolStripMenuItem.Text = Tools.MyConfig.ShowConsole ? "Hide Console" : "Show Console";
        }
        #endregion
    }
}
