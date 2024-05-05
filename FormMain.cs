using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Ez_PPPwn
{
    public partial class FormMain : Form
    {
        private string pppwnPath = string.Empty;
        private string offsetsPath = string.Empty;
        private string stage1Path = string.Empty;
        private string stage2Path = string.Empty;
        private string[] ppwnFirmwareSupport = [];
        private string[] offsettsFirmwareSupport = [];
        private string defaultFirmware = string.Empty;
        private bool comboboxFirmwareChanging = false;

        public FormMain()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }
        #region CONTROLS
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
        private void ButtonShowNetwork_Click(object sender, EventArgs e)
        {
            ShowNetwork();
        }
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            Execute();
        }
        #endregion
        #region COMBOBOX
        private void ComboBoxEthernet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEthernet.Text == "Select Ethernet Controller" | string.IsNullOrEmpty(comboBoxEthernet.Text))
            {
                MessageBox.Show("Select Ethernet Controller", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            RefreshForm();
        }
        private void ComboBoxFirmware_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboboxFirmwareChanging)
            {
                RefreshForm();
            }
        }
        #endregion
        #region TOOLSTRIP
        private async void AllToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await SaveAllTo();
        }
        private async void BatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await SaveBatch();
        }
        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }
        private async void PythonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await SavePythons();
        }
        #endregion
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
            RefreshForm();
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
                    RefreshForm();
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
                    if (offsettsFirmwareSupport == null || offsettsFirmwareSupport.Length <= 0)
                    {
                        MessageBox.Show($"{Path.GetFileName(filename)} not supported.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    offsetsPath = filename;
                    textBoxOffsets.Text = filename;
                    RefreshForm();
                    CheckFirmwares();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckConfig(string pathConfig)
        {
            if(File.Exists(pathConfig))
            {
                Config? config = Tools.GetConfig(pathConfig);
                if(config != null)
                {
                    if(File.Exists(config.Infos.PathToScript))
                    {
                        FirmwaresScriptInfos? firmwaresScriptInfos = Tools.GetFirmwareFromScript(config.Infos.PathToScript);

                        if (firmwaresScriptInfos != null && firmwaresScriptInfos.Firmwares.Length >= 0)
                        {
                            pppwnPath = config.Infos.PathToScript;
                            ppwnFirmwareSupport = firmwaresScriptInfos.Firmwares;
                            defaultFirmware = firmwaresScriptInfos.DefaultFirmware;
                        }
                        else
                        {
                            pppwnPath = string.Empty;
                            ppwnFirmwareSupport = [];
                        }

                        if (File.Exists(config.Infos.PathToOffsets))
                        {
                            offsettsFirmwareSupport = Tools.GetFirmwareFromOffsets(config.Infos.PathToOffsets);
                            if (offsettsFirmwareSupport != null && offsettsFirmwareSupport.Length >= 0)
                            {
                                offsetsPath = config.Infos.PathToOffsets;
                            }
                            else
                            {
                                offsettsFirmwareSupport = [];
                                offsetsPath = string.Empty;
                            }
                        }

                        CheckFirmwares();

                        if (config.Infos.NetworkInterface != null && config.Infos.NetworkInterface != string.Empty && comboBoxEthernet.Items.Contains(config.Infos.NetworkInterface))
                        {
                            comboBoxEthernet.SelectedItem = config.Infos.NetworkInterface;
                        }

                        if (config.Infos.Firmware != null && config.Infos.Firmware != string.Empty && comboBoxFirmware.Items.Contains(config.Infos.Firmware))
                        {
                            comboBoxFirmware.SelectedItem = config.Infos.Firmware;
                        }

                        if (File.Exists(config.Infos.Stage1Path))
                        {
                            stage1Path = config.Infos.Stage1Path;
                            textBoxStage1.Text = stage1Path;
                        }

                        if (File.Exists(config.Infos.Stage2Path))
                        {
                            stage2Path = config.Infos.Stage2Path;
                            textBoxStage2.Text = config.Infos.Stage2Path;
                        }

                        RefreshForm();
                    }

                }
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
            //else
            //{
            //    pppwnPath = string.Empty;
            //    offsetsPath = string.Empty;
            //}
        }
        private static int CheckForm(ExecuteInfos infos)
        {
            if (infos.PathToScript == null || !File.Exists(infos.PathToScript))
            {
                return -3;
            }
            else if (infos.PathToOffsets == null || !File.Exists(infos.PathToOffsets))
            {
                return -2;
            }
            else if (infos.Firmware == null || infos.Firmware == string.Empty
                    || infos.Stage1Path == null || !File.Exists(infos.Stage1Path) || infos.Stage2Path == null || !File.Exists(infos.Stage2Path))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        private void Execute()
        {
            FormConsole formConsole = new(new ExecuteInfos(pppwnPath, offsetsPath, $"{comboBoxEthernet.SelectedItem}", $"{comboBoxFirmware.SelectedItem}", stage1Path, stage2Path));
            formConsole.FormClosing += new FormClosingEventHandler((object? sender2, FormClosingEventArgs e2) =>
            {
                if (formConsole.th != null && formConsole.th.IsAlive)
                {
                    try
                    {
                        if (formConsole.th.ThreadState != System.Threading.ThreadState.Stopped)
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
        private void Init()
        {
            textBoxScript.Text = string.Empty;
            comboBoxEthernet.Items.Clear();
            comboBoxFirmware.SelectedIndex = 0;
            textBoxStage1.Text = string.Empty;
            RefreshNetwork(true);
            CheckConfig(Path.Combine(Environment.CurrentDirectory, "config.json"));
        }
        private void RefreshForm()
        {
            switch(CheckForm(new(pppwnPath, offsetsPath, $"{ comboBoxEthernet.SelectedItem }", $"{comboBoxFirmware.SelectedItem}", stage1Path, stage2Path)))
            {
                case -3:
                    Height = 160;
                    allToToolStripMenuItem.Visible = false;
                    batchToolStripMenuItem.Visible = false;
                    buttonBrowseOffsets.Enabled = false;
                    configToolStripMenuItem.Visible = false;
                    pythonsToolStripMenuItem.Visible = false;
                    saveToolStripMenuItem.Visible = false;
                    break;
                case -2:
                    Height = 160;
                    allToToolStripMenuItem.Visible = false;
                    batchToolStripMenuItem.Visible = false;
                    buttonBrowseOffsets.Enabled = true;
                    buttonStart.Visible = false;
                    configToolStripMenuItem.Visible = false;
                    pythonsToolStripMenuItem.Visible = false;
                    saveToolStripMenuItem.Visible = false;
                    textBoxScript.Text = pppwnPath;
                    break;
                case -1:
                    Height = 375;
                    allToToolStripMenuItem.Visible = false;
                    batchToolStripMenuItem.Visible = false;
                    buttonBrowseOffsets.Enabled = true;
                    configToolStripMenuItem.Visible = false;
                    pythonsToolStripMenuItem.Visible = true;
                    saveToolStripMenuItem.Visible = true;

                    textBoxScript.Text = pppwnPath;
                    textBoxOffsets.Text = offsetsPath;
                    break;
                case 0:
                    Height = 375;
                    allToToolStripMenuItem.Visible = true;
                    batchToolStripMenuItem.Visible = true;
                    buttonStart.Visible = true;
                    buttonBrowseOffsets.Enabled = true;
                    configToolStripMenuItem.Visible = true;
                    pythonsToolStripMenuItem.Visible = true;
                    saveToolStripMenuItem.Visible = true;

                    textBoxScript.Text = pppwnPath;
                    textBoxOffsets.Text = offsetsPath;
                    break;
            }
        }
        private void RefreshNetwork(bool init = false)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics == null || nics.Length < 1)
            {
                MessageBox.Show("No network interfaces found.", "Network", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                comboBoxEthernet.Items.Clear();
                foreach (NetworkInterface adapter in nics)
                {
                    if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        comboBoxEthernet.Items.Add(adapter.Description);
                    }
                }
            }
            if(!init)
            {
                RefreshForm();
            }
        }
        private async Task SaveAllTo()
        {
            FolderBrowserDialog folderBrowserDialog = new();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string pathOut = folderBrowserDialog.SelectedPath;
                if( await Tools.SaveAllScripts(pathOut, pppwnPath, offsetsPath, $"{comboBoxEthernet.SelectedItem}", $"{comboBoxFirmware.SelectedItem}", stage1Path, stage2Path, true))
                {
                    pppwnPath = Path.Combine(pathOut, Path.GetFileName(pppwnPath));
                    textBoxScript.Text = pppwnPath;
                    offsetsPath = Path.Combine(pathOut, Path.GetFileName(offsetsPath));
                    textBoxOffsets.Text = offsetsPath;
                    stage1Path = Path.Combine(pathOut, Path.GetFileName(stage1Path));
                    textBoxStage1.Text = stage1Path;
                    stage2Path = Path.Combine(pathOut, Path.GetFileName(stage2Path));
                    textBoxStage2.Text = stage2Path;
                    MessageBox.Show($"Save Successful to {pathOut} !", "Save All To", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error Save to {pathOut}", "Save All To", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            return;
        }
        private async Task SaveBatch()
        {
            SaveFileDialog saveFileDialog = new()
            {
                Title = "Save Batch As",
                Filter = "Shell (*.sh)|*.sh|Batch (*.bat)|*.bat",
                FilterIndex = 1
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
               if( await Tools.SaveShell(saveFileDialog.FileName, pppwnPath, $"{comboBoxEthernet.SelectedItem}", $"{comboBoxFirmware.SelectedItem}", stage1Path, stage2Path, true))
                {
                    MessageBox.Show($"Save {saveFileDialog.FileName} Successful !", "Save Batch As", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error Save {saveFileDialog.FileName}", "Save Batch As", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void SaveConfig()
        {
            if(Tools.SaveConfig(new(new(pppwnPath, offsetsPath, $"{comboBoxEthernet.SelectedItem}", $"{comboBoxFirmware.SelectedItem}", stage1Path, stage2Path)), Path.Combine(Environment.CurrentDirectory,"config.json")))
            {
                MessageBox.Show($"Save config.json Successful !", "Save Config", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Error Save config.json", "Save Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task SavePythons()
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
                if (directoryName != null)
                {
                    if(await Tools.SavePythonScripts(saveFileDialog.FileName, pppwnPath, offsetsPath, $"{comboBoxFirmware.SelectedItem}", true))
                    {
                        MessageBox.Show($"Save {saveFileDialog.FileName} and {Path.Combine(directoryName, "offsets.py")} Successful !", "Save Pythons As", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBoxScript.Text = saveFileDialog.FileName;
                        textBoxOffsets.Text = Path.Combine(directoryName, "offsets.py");
                    }
                }
                else
                {
                    MessageBox.Show($"Error Save {saveFileDialog.FileName}", "Save Pythons As", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private static void ShowNetwork()
        {
            Process.Start(@"Rundll32.exe", " shell32.dll,Control_RunDLL ncpa.cpl");
        }
        [GeneratedRegex("[^0-9]")]
        private static partial Regex RegexSortNum();
        #endregion
        
    }
}


