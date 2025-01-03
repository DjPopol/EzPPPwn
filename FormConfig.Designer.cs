
using EzPPPwn.Models;

namespace EzPPPwn
{
    partial class FormConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            groupBoxNetworkInterface = new GroupBox();
            buttonRefreshNetwork = new Button();
            buttonNetwork = new Button();
            comboBoxEthernet = new ComboBox();
            buttonContinue = new Button();
            buttonCancel = new Button();
            groupBoxPPPwnOptions = new GroupBox();
            checkBoxBufferSize = new CheckBox();
            numericUpDownBufferSize = new NumericUpDown();
            checkBoxOldIpv6 = new CheckBox();
            buttonDefaultPPPwn = new Button();
            checkBoxGroomDelay = new CheckBox();
            checkBoxTimeOut = new CheckBox();
            checkBoxReelSleep = new CheckBox();
            numericUpDownTimeOut = new NumericUpDown();
            checkBoxWaitAfterPin = new CheckBox();
            checkBoxNoWaitPadi = new CheckBox();
            numericUpDownWaitAfterPin = new NumericUpDown();
            numericUpDownGroomDelay = new NumericUpDown();
            checkBoxAutoRetry = new CheckBox();
            groupBoxFirmware = new GroupBox();
            comboBoxFirmware = new ComboBox();
            groupBoxStage2 = new GroupBox();
            textBoxStage2 = new TextBox();
            buttonBrowseStage2 = new Button();
            groupBoxNetworkInterface.SuspendLayout();
            groupBoxPPPwnOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownBufferSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTimeOut).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWaitAfterPin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownGroomDelay).BeginInit();
            groupBoxFirmware.SuspendLayout();
            groupBoxStage2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxNetworkInterface
            // 
            groupBoxNetworkInterface.BackColor = Color.Transparent;
            groupBoxNetworkInterface.Controls.Add(buttonRefreshNetwork);
            groupBoxNetworkInterface.Controls.Add(buttonNetwork);
            groupBoxNetworkInterface.Controls.Add(comboBoxEthernet);
            groupBoxNetworkInterface.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxNetworkInterface.ForeColor = Color.White;
            groupBoxNetworkInterface.Location = new Point(12, 2);
            groupBoxNetworkInterface.Name = "groupBoxNetworkInterface";
            groupBoxNetworkInterface.Size = new Size(457, 50);
            groupBoxNetworkInterface.TabIndex = 0;
            groupBoxNetworkInterface.TabStop = false;
            groupBoxNetworkInterface.Text = "Network Interface";
            // 
            // buttonRefreshNetwork
            // 
            buttonRefreshNetwork.BackColor = Color.LightGray;
            buttonRefreshNetwork.FlatAppearance.BorderColor = Color.DimGray;
            buttonRefreshNetwork.FlatStyle = FlatStyle.Flat;
            buttonRefreshNetwork.ForeColor = Color.DodgerBlue;
            buttonRefreshNetwork.Location = new Point(6, 21);
            buttonRefreshNetwork.Name = "buttonRefreshNetwork";
            buttonRefreshNetwork.Size = new Size(78, 22);
            buttonRefreshNetwork.TabIndex = 5;
            buttonRefreshNetwork.Text = "Refresh";
            buttonRefreshNetwork.UseVisualStyleBackColor = false;
            buttonRefreshNetwork.Click += ButtonRefreshNetwork_Click;
            // 
            // buttonNetwork
            // 
            buttonNetwork.BackColor = Color.LightGray;
            buttonNetwork.FlatAppearance.BorderColor = Color.DimGray;
            buttonNetwork.FlatStyle = FlatStyle.Flat;
            buttonNetwork.ForeColor = Color.DodgerBlue;
            buttonNetwork.Location = new Point(373, 21);
            buttonNetwork.Name = "buttonNetwork";
            buttonNetwork.Size = new Size(78, 22);
            buttonNetwork.TabIndex = 4;
            buttonNetwork.Text = "Show";
            buttonNetwork.UseVisualStyleBackColor = false;
            buttonNetwork.Click += ButtonShowNetwork_Click;
            // 
            // comboBoxEthernet
            // 
            comboBoxEthernet.BackColor = SystemColors.ControlLight;
            comboBoxEthernet.DisplayMember = "Name";
            comboBoxEthernet.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEthernet.FormattingEnabled = true;
            comboBoxEthernet.Location = new Point(107, 21);
            comboBoxEthernet.Name = "comboBoxEthernet";
            comboBoxEthernet.Size = new Size(260, 21);
            comboBoxEthernet.TabIndex = 3;
            comboBoxEthernet.SelectedIndexChanged += ComboBoxEthernet_SelectedIndexChanged;
            // 
            // buttonContinue
            // 
            buttonContinue.BackColor = Color.LightGray;
            buttonContinue.FlatAppearance.BorderColor = Color.DimGray;
            buttonContinue.FlatStyle = FlatStyle.Flat;
            buttonContinue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonContinue.ForeColor = Color.DodgerBlue;
            buttonContinue.Location = new Point(385, 255);
            buttonContinue.Name = "buttonContinue";
            buttonContinue.Size = new Size(78, 22);
            buttonContinue.TabIndex = 20;
            buttonContinue.Text = "Save";
            buttonContinue.UseCompatibleTextRendering = true;
            buttonContinue.UseVisualStyleBackColor = false;
            buttonContinue.Click += ButtonSave_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = Color.LightGray;
            buttonCancel.FlatAppearance.BorderColor = Color.DimGray;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.DodgerBlue;
            buttonCancel.Location = new Point(301, 255);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(78, 22);
            buttonCancel.TabIndex = 24;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseCompatibleTextRendering = true;
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // groupBoxPPPwnOptions
            // 
            groupBoxPPPwnOptions.Controls.Add(checkBoxBufferSize);
            groupBoxPPPwnOptions.Controls.Add(numericUpDownBufferSize);
            groupBoxPPPwnOptions.Controls.Add(checkBoxOldIpv6);
            groupBoxPPPwnOptions.Controls.Add(buttonDefaultPPPwn);
            groupBoxPPPwnOptions.Controls.Add(checkBoxGroomDelay);
            groupBoxPPPwnOptions.Controls.Add(checkBoxTimeOut);
            groupBoxPPPwnOptions.Controls.Add(checkBoxReelSleep);
            groupBoxPPPwnOptions.Controls.Add(numericUpDownTimeOut);
            groupBoxPPPwnOptions.Controls.Add(checkBoxWaitAfterPin);
            groupBoxPPPwnOptions.Controls.Add(checkBoxNoWaitPadi);
            groupBoxPPPwnOptions.Controls.Add(numericUpDownWaitAfterPin);
            groupBoxPPPwnOptions.Controls.Add(numericUpDownGroomDelay);
            groupBoxPPPwnOptions.Controls.Add(checkBoxAutoRetry);
            groupBoxPPPwnOptions.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxPPPwnOptions.ForeColor = Color.White;
            groupBoxPPPwnOptions.Location = new Point(12, 102);
            groupBoxPPPwnOptions.Name = "groupBoxPPPwnOptions";
            groupBoxPPPwnOptions.Size = new Size(457, 147);
            groupBoxPPPwnOptions.TabIndex = 27;
            groupBoxPPPwnOptions.TabStop = false;
            groupBoxPPPwnOptions.Text = "Options";
            // 
            // checkBoxBufferSize
            // 
            checkBoxBufferSize.AutoSize = true;
            checkBoxBufferSize.Location = new Point(206, 113);
            checkBoxBufferSize.Name = "checkBoxBufferSize";
            checkBoxBufferSize.Size = new Size(81, 17);
            checkBoxBufferSize.TabIndex = 43;
            checkBoxBufferSize.Text = "Buffer Size";
            checkBoxBufferSize.UseVisualStyleBackColor = true;
            // 
            // numericUpDownBufferSize
            // 
            numericUpDownBufferSize.ForeColor = Color.DodgerBlue;
            numericUpDownBufferSize.Location = new Point(311, 112);
            numericUpDownBufferSize.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            numericUpDownBufferSize.Name = "numericUpDownBufferSize";
            numericUpDownBufferSize.Size = new Size(56, 22);
            numericUpDownBufferSize.TabIndex = 42;
            // 
            // checkBoxOldIpv6
            // 
            checkBoxOldIpv6.AutoSize = true;
            checkBoxOldIpv6.FlatAppearance.CheckedBackColor = Color.Transparent;
            checkBoxOldIpv6.Location = new Point(107, 113);
            checkBoxOldIpv6.Name = "checkBoxOldIpv6";
            checkBoxOldIpv6.Size = new Size(69, 17);
            checkBoxOldIpv6.TabIndex = 41;
            checkBoxOldIpv6.Text = "Old Ipv6";
            checkBoxOldIpv6.UseVisualStyleBackColor = true;
            // 
            // buttonDefaultPPPwn
            // 
            buttonDefaultPPPwn.BackColor = Color.LightGray;
            buttonDefaultPPPwn.FlatAppearance.BorderColor = Color.DimGray;
            buttonDefaultPPPwn.FlatStyle = FlatStyle.Flat;
            buttonDefaultPPPwn.ForeColor = Color.DodgerBlue;
            buttonDefaultPPPwn.Location = new Point(6, 44);
            buttonDefaultPPPwn.Name = "buttonDefaultPPPwn";
            buttonDefaultPPPwn.Size = new Size(78, 22);
            buttonDefaultPPPwn.TabIndex = 33;
            buttonDefaultPPPwn.Text = "Set Default";
            buttonDefaultPPPwn.UseVisualStyleBackColor = false;
            buttonDefaultPPPwn.Click += ButtonDefaultPPPwn_Click;
            // 
            // checkBoxGroomDelay
            // 
            checkBoxGroomDelay.AutoSize = true;
            checkBoxGroomDelay.Location = new Point(206, 16);
            checkBoxGroomDelay.Name = "checkBoxGroomDelay";
            checkBoxGroomDelay.Size = new Size(93, 17);
            checkBoxGroomDelay.TabIndex = 39;
            checkBoxGroomDelay.Text = "Groom delay";
            checkBoxGroomDelay.UseVisualStyleBackColor = true;
            // 
            // checkBoxTimeOut
            // 
            checkBoxTimeOut.AutoSize = true;
            checkBoxTimeOut.Location = new Point(206, 81);
            checkBoxTimeOut.Name = "checkBoxTimeOut";
            checkBoxTimeOut.Size = new Size(72, 17);
            checkBoxTimeOut.TabIndex = 37;
            checkBoxTimeOut.Text = "Time out";
            checkBoxTimeOut.UseVisualStyleBackColor = true;
            // 
            // checkBoxReelSleep
            // 
            checkBoxReelSleep.AutoSize = true;
            checkBoxReelSleep.Location = new Point(107, 81);
            checkBoxReelSleep.Name = "checkBoxReelSleep";
            checkBoxReelSleep.Size = new Size(78, 17);
            checkBoxReelSleep.TabIndex = 40;
            checkBoxReelSleep.Text = "Real sleep";
            checkBoxReelSleep.UseVisualStyleBackColor = true;
            // 
            // numericUpDownTimeOut
            // 
            numericUpDownTimeOut.ForeColor = Color.DodgerBlue;
            numericUpDownTimeOut.Location = new Point(311, 80);
            numericUpDownTimeOut.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            numericUpDownTimeOut.Name = "numericUpDownTimeOut";
            numericUpDownTimeOut.Size = new Size(32, 22);
            numericUpDownTimeOut.TabIndex = 36;
            // 
            // checkBoxWaitAfterPin
            // 
            checkBoxWaitAfterPin.AutoSize = true;
            checkBoxWaitAfterPin.Location = new Point(206, 49);
            checkBoxWaitAfterPin.Name = "checkBoxWaitAfterPin";
            checkBoxWaitAfterPin.Size = new Size(97, 17);
            checkBoxWaitAfterPin.TabIndex = 35;
            checkBoxWaitAfterPin.Text = "Wait after pin";
            checkBoxWaitAfterPin.UseVisualStyleBackColor = true;
            // 
            // checkBoxNoWaitPadi
            // 
            checkBoxNoWaitPadi.AutoSize = true;
            checkBoxNoWaitPadi.Location = new Point(107, 49);
            checkBoxNoWaitPadi.Name = "checkBoxNoWaitPadi";
            checkBoxNoWaitPadi.Size = new Size(95, 17);
            checkBoxNoWaitPadi.TabIndex = 33;
            checkBoxNoWaitPadi.Text = "No wait PADI";
            checkBoxNoWaitPadi.UseVisualStyleBackColor = true;
            // 
            // numericUpDownWaitAfterPin
            // 
            numericUpDownWaitAfterPin.ForeColor = Color.DodgerBlue;
            numericUpDownWaitAfterPin.Location = new Point(311, 48);
            numericUpDownWaitAfterPin.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            numericUpDownWaitAfterPin.Name = "numericUpDownWaitAfterPin";
            numericUpDownWaitAfterPin.Size = new Size(32, 22);
            numericUpDownWaitAfterPin.TabIndex = 34;
            numericUpDownWaitAfterPin.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericUpDownGroomDelay
            // 
            numericUpDownGroomDelay.ForeColor = Color.DodgerBlue;
            numericUpDownGroomDelay.Location = new Point(311, 16);
            numericUpDownGroomDelay.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownGroomDelay.Name = "numericUpDownGroomDelay";
            numericUpDownGroomDelay.Size = new Size(32, 22);
            numericUpDownGroomDelay.TabIndex = 38;
            numericUpDownGroomDelay.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // checkBoxAutoRetry
            // 
            checkBoxAutoRetry.AutoSize = true;
            checkBoxAutoRetry.FlatAppearance.CheckedBackColor = Color.Transparent;
            checkBoxAutoRetry.Location = new Point(107, 16);
            checkBoxAutoRetry.Name = "checkBoxAutoRetry";
            checkBoxAutoRetry.Size = new Size(83, 17);
            checkBoxAutoRetry.TabIndex = 32;
            checkBoxAutoRetry.Text = "Auto-Retry";
            checkBoxAutoRetry.UseVisualStyleBackColor = true;
            // 
            // groupBoxFirmware
            // 
            groupBoxFirmware.Controls.Add(comboBoxFirmware);
            groupBoxFirmware.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxFirmware.ForeColor = Color.White;
            groupBoxFirmware.Location = new Point(12, 53);
            groupBoxFirmware.Name = "groupBoxFirmware";
            groupBoxFirmware.Size = new Size(92, 48);
            groupBoxFirmware.TabIndex = 25;
            groupBoxFirmware.TabStop = false;
            groupBoxFirmware.Text = "Firmware";
            // 
            // comboBoxFirmware
            // 
            comboBoxFirmware.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxFirmware.BackColor = Color.LightGray;
            comboBoxFirmware.DisplayMember = "FwWithPoint";
            comboBoxFirmware.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFirmware.Location = new Point(6, 17);
            comboBoxFirmware.Name = "comboBoxFirmware";
            comboBoxFirmware.RightToLeft = RightToLeft.Yes;
            comboBoxFirmware.Size = new Size(78, 21);
            comboBoxFirmware.TabIndex = 6;
            // 
            // groupBoxStage2
            // 
            groupBoxStage2.Controls.Add(textBoxStage2);
            groupBoxStage2.Controls.Add(buttonBrowseStage2);
            groupBoxStage2.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxStage2.ForeColor = Color.White;
            groupBoxStage2.Location = new Point(109, 53);
            groupBoxStage2.Name = "groupBoxStage2";
            groupBoxStage2.Size = new Size(360, 48);
            groupBoxStage2.TabIndex = 26;
            groupBoxStage2.TabStop = false;
            groupBoxStage2.Text = "Stage2";
            // 
            // textBoxStage2
            // 
            textBoxStage2.Location = new Point(10, 17);
            textBoxStage2.Name = "textBoxStage2";
            textBoxStage2.ReadOnly = true;
            textBoxStage2.Size = new Size(260, 22);
            textBoxStage2.TabIndex = 0;
            textBoxStage2.TabStop = false;
            // 
            // buttonBrowseStage2
            // 
            buttonBrowseStage2.BackColor = Color.LightGray;
            buttonBrowseStage2.FlatAppearance.BorderColor = Color.DimGray;
            buttonBrowseStage2.FlatStyle = FlatStyle.Flat;
            buttonBrowseStage2.ForeColor = Color.DodgerBlue;
            buttonBrowseStage2.Location = new Point(276, 17);
            buttonBrowseStage2.Name = "buttonBrowseStage2";
            buttonBrowseStage2.Size = new Size(78, 22);
            buttonBrowseStage2.TabIndex = 8;
            buttonBrowseStage2.Text = "Browse";
            buttonBrowseStage2.UseVisualStyleBackColor = false;
            buttonBrowseStage2.Click += ButtonBrowseStage2_Click;
            // 
            // FormConfig
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DodgerBlue;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(480, 288);
            Controls.Add(groupBoxPPPwnOptions);
            Controls.Add(groupBoxFirmware);
            Controls.Add(groupBoxStage2);
            Controls.Add(buttonCancel);
            Controls.Add(buttonContinue);
            Controls.Add(groupBoxNetworkInterface);
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormConfig";
            Shown += FormConfig_Shown;
            groupBoxNetworkInterface.ResumeLayout(false);
            groupBoxPPPwnOptions.ResumeLayout(false);
            groupBoxPPPwnOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownBufferSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTimeOut).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWaitAfterPin).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownGroomDelay).EndInit();
            groupBoxFirmware.ResumeLayout(false);
            groupBoxStage2.ResumeLayout(false);
            groupBoxStage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxNetworkInterface;
        private System.Windows.Forms.Button buttonRefreshNetwork;
        private System.Windows.Forms.Button buttonNetwork;
        private System.Windows.Forms.ComboBox comboBoxEthernet;
        private Button buttonContinue;
        private Button buttonCancel;
        private GroupBox groupBoxPPPwnOptions;
        private Button buttonDefaultPPPwn;
        private CheckBox checkBoxGroomDelay;
        private CheckBox checkBoxTimeOut;
        private CheckBox checkBoxReelSleep;
        private NumericUpDown numericUpDownTimeOut;
        private CheckBox checkBoxWaitAfterPin;
        private CheckBox checkBoxNoWaitPadi;
        private NumericUpDown numericUpDownWaitAfterPin;
        private NumericUpDown numericUpDownGroomDelay;
        private CheckBox checkBoxAutoRetry;
        private GroupBox groupBoxFirmware;
        private ComboBox comboBoxFirmware;
        private GroupBox groupBoxStage2;
        private TextBox textBoxStage2;
        private Button buttonBrowseStage2;
        private CheckBox checkBoxOldIpv6;
        private CheckBox checkBoxBufferSize;
        private NumericUpDown numericUpDownBufferSize;
    }
}

