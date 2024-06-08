using EzPPPwn.Helpers;
using EzPPPwn.Models;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace EzPPPwn
{
    public partial class FormConfig : Form
    {
        private readonly bool comboboxFirmwareLock = false;
        public FormConfig()
        {
            InitializeComponent();
        }
        private void FormConfig_Shown(object sender, EventArgs e)
        {
            Init();
        }
        #region CONTROLS
        #region BUTTONS
        private void ButtonBrowseStage2_Click(object sender, EventArgs e)
        {
            BrowsePayload();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (!Tools.MyConfig.CheckConfig())
            {
                DialogResult dialogResult = MessageBox.Show("Config not completed\nExit EzPPPwn ?", "Config", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.Yes)
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
        }
        private void ButtonDefaultPPPwn_Click(object sender, EventArgs e)
        {
            checkBoxAutoRetry.Checked = true;
            checkBoxGroomDelay.Checked = true;
            numericUpDownGroomDelay.Value = 4;
            checkBoxNoWaitPadi.Checked = true;
            checkBoxReelSleep.Checked = false;
            checkBoxTimeOut.Checked = false;
            numericUpDownTimeOut.Value = 0;
            checkBoxWaitAfterPin.Checked = true;
            numericUpDownWaitAfterPin.Value = 1;
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveConfig(true);
        }
        private void ButtonRefreshNetwork_Click(object sender, EventArgs e)
        {
            RefreshNetwork();
        }
        private void ButtonShowNetwork_Click(object sender, EventArgs e)
        {
            ShowNetwork();
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
            if (!comboboxFirmwareLock)
            {
                RefreshForm();
            }
        }
        #endregion
        #endregion
        #region FUNCTIONS
        private void BrowsePayload()
        {
            OpenFileDialog opendialog = new()
            {
                CheckFileExists = true,
                Multiselect = false,
                Title = $"Select stage2.bin file",
                Filter = $"PS4 STAGE BIN File (stage2*.bin)|stage2*.bin|BIN file (*.bin)|*.bin",
            };
            if (opendialog.ShowDialog() == DialogResult.OK)
            {
                textBoxStage2.Text = opendialog.FileName;
            }
            RefreshForm();
        }
        private bool CheckConfig()
        {
            bool result = true;
            if (Tools.MyConfig != null)
            {
                if (Tools.MyConfig.NetworkInterface != null && comboBoxEthernet.Items.Cast<NetworkInterface>().First(i => i.Description == Tools.MyConfig.NetworkInterface.Description) != null)
                {
                    comboBoxEthernet.SelectedItem = comboBoxEthernet.Items.Cast<NetworkInterface>().First(i => i.Description == Tools.MyConfig.NetworkInterface.Description);
                }
                else if (comboBoxEthernet.Items.Count > 0)
                {
                    comboBoxEthernet.SelectedIndex = 0;
                    result = false;
                }
                else
                {
                    result = false;
                }
                if (Tools.MyConfig.Firmware != null && comboBoxFirmware.Items.Cast<Firmware>().First(i => i.Fw == Tools.MyConfig.Firmware.Fw) != null)
                {
                    comboBoxFirmware.SelectedItem = comboBoxFirmware.Items.Cast<Firmware>().First(i => i.Fw == Tools.MyConfig.Firmware.Fw);
                }
                else if (comboBoxFirmware.Items.Count > 0)
                {
                    comboBoxFirmware.SelectedIndex = 0;
                    result = false;
                }
                else
                {
                    result = false;
                }
                if (Tools.MyConfig.Stage2Path != string.Empty && File.Exists(Tools.MyConfig.Stage2Path))
                {
                    textBoxStage2.Text = Tools.MyConfig.Stage2Path;
                }
                else
                {
                    result = false;
                }
                checkBoxAutoRetry.Checked = Tools.MyConfig.PPPwnConfig.UseAutoRetry;
                checkBoxGroomDelay.Checked = Tools.MyConfig.PPPwnConfig.UseGroomDelay;
                numericUpDownGroomDelay.Value = Tools.MyConfig.PPPwnConfig.GroomDelay;
                checkBoxNoWaitPadi.Checked = Tools.MyConfig.PPPwnConfig.UseNoWaitPADI;
                checkBoxReelSleep.Checked = Tools.MyConfig.PPPwnConfig.UseRealSleep;
                checkBoxTimeOut.Checked = Tools.MyConfig.PPPwnConfig.UseTimeOut;
                numericUpDownTimeOut.Value = Tools.MyConfig.PPPwnConfig.TimeOut;
                checkBoxWaitAfterPin.Checked = Tools.MyConfig.PPPwnConfig.UseWaitAfterPIN;
                numericUpDownWaitAfterPin.Value = Tools.MyConfig.PPPwnConfig.WaitAfterPIN;
            }
            return result;
        }
        private static bool CheckForm(NetworkInterface networkInterface, Firmware firmware, string stage2Path)
        {
            if (networkInterface == null || firmware == null
                    || stage2Path == null || !File.Exists(stage2Path))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void Init()
        {
            comboBoxEthernet.Items.Clear();
            comboBoxFirmware.Items.Clear();
            Firmware[] firmwares = new Firmware[Firmware.SupportedFws.Length];
            for (int i = 0; i < Firmware.SupportedFws.Length; i++)
            {
                firmwares[i] = new(Firmware.SupportedFws[i]);
            }
            Array.Reverse(firmwares);
            comboBoxFirmware.Items.AddRange(firmwares);
            RefreshNetwork(true);
            CheckConfig();
            RefreshForm();
        }
        private void RefreshForm()
        {
            if (comboBoxEthernet.SelectedItem != null && comboBoxFirmware.SelectedItem != null)
            {
                bool result = CheckForm((NetworkInterface)comboBoxEthernet.SelectedItem, (Firmware)comboBoxFirmware.SelectedItem, textBoxStage2.Text);
                buttonContinue.Visible = result;
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
                        Debug.WriteLine($"{adapter.Description} :\nID={adapter.Id}\nName={adapter.Name}\nPhysical Adress={adapter.GetPhysicalAddress().ToString()}\n\n");
                        comboBoxEthernet.Items.Add(adapter);
                    }
                }
            }
            if (!init)
            {
                RefreshForm();
            }
        }
        private void SaveConfig(bool showMsg)
        {
            if (comboBoxFirmware.SelectedItem != null && comboBoxFirmware.SelectedItem is Firmware f)
            {
                Tools.MyConfig.Firmware = f;
            }
            Tools.MyConfig.PPPwnConfig = new(checkBoxAutoRetry.Checked,
                                         checkBoxGroomDelay.Checked,
                                         (int)numericUpDownGroomDelay.Value,
                                         checkBoxNoWaitPadi.Checked,
                                         checkBoxReelSleep.Checked,
                                         checkBoxTimeOut.Checked,
                                         (int)numericUpDownTimeOut.Value,
                                         checkBoxWaitAfterPin.Checked,
                                         (int)numericUpDownWaitAfterPin.Value);

            Tools.MyConfig.SetConfig(comboBoxEthernet.SelectedItem != null ? (NetworkInterface)comboBoxEthernet.SelectedItem : null,
                Tools.MyConfig.Firmware, textBoxStage2.Text,
                Tools.MyConfig.PPPwnConfig);
            bool result = Tools.MyConfig.Save();

            if (!result && showMsg)
            {
                MessageBox.Show($"Error Save config.json", "Save Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }
        private static void ShowNetwork()
        {
            Process.Start(@"Rundll32.exe", " shell32.dll,Control_RunDLL ncpa.cpl");
        }
    #endregion
    }
}


