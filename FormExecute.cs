using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace Ez_PPPwn
{
    public partial class FormExecute : Form
    {
        private string pppwnPath = string.Empty;
        private string offsetsPath = string.Empty;
        private string stage1Path = string.Empty;
        private string stage2Path = string.Empty;
        private string[] ppwnFirmwareSupport = [];
        private string[] offsettsFirmwareSupport = [];
        private string defaultFirmware = string.Empty;
        private bool comboboxFirmwareChanging = false;
        public FormExecute()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }
        #region BUTTONS
        private void ButtonBrowseOffsets_Click(object sender, EventArgs e)
        {
            BrowseOffsets();
        }
        private void ButtonBrowsePPPwn_Click(object sender, EventArgs e)
        {
            BrowsePPPwn();
        }
        private void ButtonBrowseStage1_Click(object sender, EventArgs e)
        {
            BrowsePayload(1);
        }
        private void ButtonBrowseStage2_Click(object sender, EventArgs e)
        {
            BrowsePayload(2);
        }
        private void ButtonRefreshNetwork_Click(object sender, EventArgs e)
        {
            RefreshNetwork();
        }
        private async void ButtonSaveShell_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Title = "Save Batch As",
                Filter = "Shell (*.sh)|*.sh|Batch (*.bat)|*.bat",
                FilterIndex = 1
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                await Tools.SaveShell(saveFileDialog.FileName, pppwnPath, $"{comboBoxEthernet.SelectedItem}", $"{comboBoxFirmware.SelectedItem}", stage1Path, stage2Path, true);
            }
        }
        private async void ButtonSavePythons_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Title = "Save Python As",
                Filter = "Python (*.py)|*.py",
                FilterIndex = 1
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string? directoryName = Path.GetDirectoryName(saveFileDialog.FileName);
                if(directoryName != null)
                {
                    await Tools.SavePythonScripts(saveFileDialog.FileName, pppwnPath, offsetsPath, $"{comboBoxFirmware.SelectedItem}", true);
                    textBoxScript.Text = saveFileDialog.FileName;
                    textBoxOffsets.Text = Path.Combine(directoryName, "offsets.py");
                }
                
            }
        }
        private async void ButtonSaveAllScripts_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string pathOut = folderBrowserDialog.SelectedPath;
                await Tools.SaveAllScripts(pathOut, pppwnPath, offsetsPath, $"{comboBoxEthernet.SelectedItem}", $"{comboBoxFirmware.SelectedItem}", stage1Path, stage2Path, true);
                pppwnPath = Path.Combine(pathOut, Path.GetFileName(pppwnPath));
                textBoxScript.Text =pppwnPath;
                offsetsPath = Path.Combine(pathOut, Path.GetFileName(offsetsPath));
                textBoxOffsets.Text = offsetsPath;
                stage1Path = Path.Combine(pathOut, Path.GetFileName(stage1Path));
                textBoxStage1.Text = stage1Path;
                stage2Path = Path.Combine(pathOut, Path.GetFileName(stage2Path));
                textBoxStage2.Text = stage2Path;
            }
        }
        private void ButtonShowNetwork_Click(object sender, EventArgs e)
        {
            Process.Start(@"Rundll32.exe", " shell32.dll,Control_RunDLL ncpa.cpl");
        }
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            FormConsole formConsole = new(new ExecuteInfos(pppwnPath, offsetsPath, $"{comboBoxEthernet.SelectedItem}", $"{comboBoxFirmware.SelectedItem}", stage1Path, stage2Path));
            formConsole.FormClosing += new FormClosingEventHandler((object? sender2, FormClosingEventArgs e2) =>
            {
                if(formConsole.th != null && formConsole.th.IsAlive)
                {
                    try
                    {
                        if(formConsole.th.ThreadState != System.Threading.ThreadState.Stopped)
                        {
                            formConsole.th.Interrupt();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Thread Interrupt :\n{ex.Message}");
                    }
                }
                Tools.CleanPathTmp();
                Enabled = true;
                Show();
            });
            Enabled = false;
            Hide();
            formConsole.Show();
        }
        #endregion
        #region COMBOBOX
        private void ComboBoxEthernet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEthernet.Text == "Select Ethernet Controller" | string.IsNullOrEmpty(comboBoxEthernet.Text))
            {
                MessageBox.Show("Select Ethernet Controller", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            CheckForm();
        }
        private void ComboBoxFirmware_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboboxFirmwareChanging)
            {
                CheckForm();
            }
        }
        #endregion
        #region FUNCTIONS
        private void BrowsePayload(int stage)
        {
            OpenFileDialog opendialog = new()
            {
                CheckFileExists = true,
                Multiselect = false,
                Title = $"Select stage{stage}.bin file",
                Filter = $"PS4 STAGE BIN File (stage{stage}.bin)|stage{stage}.bin|BIN file (*.bin)|*.bin",
            };
            if (opendialog.ShowDialog() == DialogResult.OK)
            {
                if (stage == 1)
                {
                    stage1Path = opendialog.FileName;
                    textBoxStage1.Text = opendialog.SafeFileName;
                }
                else
                {
                    stage2Path = opendialog.FileName;
                    textBoxStage2.Text = opendialog.SafeFileName;
                }
            }
            CheckForm();
        }
        private void BrowsePPPwn()
        {
            try
            {
                OpenFileDialog openFileDialog = new()
                {
                    Title = "Select your PPPwn scripts (pppwn.py)",
                    Filter = "PPPwn|pppwn.py|Python Files (*.py)|*.py",
                    FilterIndex = 1
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pppwnPath = openFileDialog.FileName; ;
                    textBoxScript.Text = pppwnPath;
                    FirmwaresScriptInfos? firmwaresScriptInfos = Tools.GetFirmwareFromScript(pppwnPath);

                    if (firmwaresScriptInfos != null && firmwaresScriptInfos.Firmwares.Length >= 0)
                    {
                        ppwnFirmwareSupport = firmwaresScriptInfos.Firmwares;
                        defaultFirmware = firmwaresScriptInfos.DefaultFirmware;
                    }
                    else
                    {
                        MessageBox.Show($"{textBoxScript.Text} not supported.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    CheckForm();
                    CheckFirmwares();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BrowseOffsets()
        {
            try
            {
                OpenFileDialog openFileDialog = new()
                {
                    Title = "Select your offsets scripts (offsets.py)",
                    Filter = "Offsets|offsets.py|Python Files (*.py)|*.py"
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog.FileName;
                    offsettsFirmwareSupport = Tools.GetFirmwareFromOffsets(filename);
                    if(offsettsFirmwareSupport == null || offsettsFirmwareSupport.Length <= 0)
                    {
                        MessageBox.Show($"{Path.GetFileName(filename)} not supported.", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }
                    offsetsPath = filename;
                    textBoxOffsets.Text = filename;
                    CheckForm();
                    CheckFirmwares();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckFirmwares()
        {
            if (ppwnFirmwareSupport.Length > 0 && offsettsFirmwareSupport.Length > 0)
            {
                comboboxFirmwareChanging = true;
                comboBoxFirmware.Items.Clear();
                Array.Sort(offsettsFirmwareSupport, (a, b) => int.Parse(RegexSortNum().Replace(a, "")) - int.Parse(RegexSortNum().Replace(b, "")));
                foreach (string firmware in offsettsFirmwareSupport)
                {
                    string f = firmware.Insert((firmware.Length == 3 ? 1 : 2), ".");
                    comboBoxFirmware.Items.Add(f);
                }

                string df = defaultFirmware.Insert((defaultFirmware.Length == 3 ? 1 : 2), ".");
                if (comboBoxFirmware.Items.Contains(df))
                {
                    comboBoxFirmware.SelectedItem = df;
                }
                comboboxFirmwareChanging = false;
            }
        }
        private void CheckForm()
        {
            if (pppwnPath == null || !File.Exists(pppwnPath))
            {
                Height = 135;
                buttonSaveAllScripts.Enabled = false;
                buttonSaveShell.Enabled = false;
                buttonSavePythons.Enabled = false;
                buttonStart.Enabled = false;
                buttonBrowseOffsets.Enabled = false;
            }
            else if (offsetsPath == null || !File.Exists(offsetsPath))
            {
                Height = 135;
                buttonSaveAllScripts.Enabled = false;
                buttonSaveShell.Enabled = false;
                buttonSavePythons.Enabled = false;
                buttonStart.Enabled = false;
                buttonBrowseOffsets.Enabled = true;
            }
            else if (comboBoxEthernet.SelectedItem == null || comboBoxFirmware.SelectedItem == null
                    || stage1Path == null || !File.Exists(stage1Path) || stage2Path == null || !File.Exists(stage2Path))
            {
                Height = 350;
                buttonSaveAllScripts.Enabled = false;
                buttonSaveShell.Enabled = false;
                buttonSavePythons.Enabled = true;
                buttonStart.Enabled = false;
                buttonBrowseOffsets.Enabled = true;
                
            }
            else
            {
                Height = 350;
                buttonSaveAllScripts.Enabled = true;
                buttonSaveShell.Enabled = true;
                buttonSavePythons.Enabled = true;
                buttonStart.Enabled = true;
                buttonBrowseOffsets.Enabled = true;
                
            }
        }
        private void Init()
        {
            textBoxScript.Text = string.Empty;
            comboBoxEthernet.Items.Clear();
            comboBoxFirmware.SelectedIndex = 0;
            textBoxStage1.Text = string.Empty;
            RefreshNetwork();
        }
        private void RefreshNetwork()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics == null || nics.Length < 1)
            {
                MessageBox.Show("No network interfaces found.", "Network", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CheckForm();
                return;
            }
            comboBoxEthernet.Items.Clear();
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    comboBoxEthernet.Items.Add(adapter.Description);
                }
            }
            CheckForm();
        }

        [GeneratedRegex("[^0-9]")]
        private static partial Regex RegexSortNum();
        #endregion
    }
}


