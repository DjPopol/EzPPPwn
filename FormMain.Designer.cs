
namespace Ez_PPPwn
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            buttonStart = new Button();
            buttonBrowsePPPwn = new Button();
            groupBoxNetworkPC = new GroupBox();
            buttonRefreshNetwork = new Button();
            buttonNetwork = new Button();
            pictureBoxPC = new PictureBox();
            comboBoxEthernet = new ComboBox();
            textBoxStage1 = new TextBox();
            buttonBrowseStage1 = new Button();
            groupBoxStage1 = new GroupBox();
            groupBoxScript = new GroupBox();
            buttonBrowseOffsets = new Button();
            textBoxOffsets = new TextBox();
            pictureBox1 = new PictureBox();
            textBoxScript = new TextBox();
            buttonSaveAllScripts = new Button();
            pictureBoxPS4 = new PictureBox();
            groupBoxNetworkPS = new GroupBox();
            comboBoxFirmware = new ComboBox();
            groupBoxStage2 = new GroupBox();
            textBoxStage2 = new TextBox();
            buttonBrowseStage2 = new Button();
            buttonSaveShell = new Button();
            buttonSavePythons = new Button();
            groupBoxNetworkPC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC).BeginInit();
            groupBoxStage1.SuspendLayout();
            groupBoxScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPS4).BeginInit();
            groupBoxNetworkPS.SuspendLayout();
            groupBoxStage2.SuspendLayout();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Enabled = false;
            buttonStart.Location = new Point(442, 283);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(139, 23);
            buttonStart.TabIndex = 15;
            buttonStart.Text = "Execute";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += ButtonStart_Click;
            // 
            // buttonBrowsePPPwn
            // 
            buttonBrowsePPPwn.Location = new Point(98, 21);
            buttonBrowsePPPwn.Name = "buttonBrowsePPPwn";
            buttonBrowsePPPwn.Size = new Size(101, 23);
            buttonBrowsePPPwn.TabIndex = 1;
            buttonBrowsePPPwn.Text = "Browse Pppwn";
            buttonBrowsePPPwn.UseVisualStyleBackColor = true;
            buttonBrowsePPPwn.Click += ButtonBrowsePPPwn_Click;
            // 
            // groupBoxNetworkPC
            // 
            groupBoxNetworkPC.BackColor = SystemColors.MenuBar;
            groupBoxNetworkPC.Controls.Add(buttonRefreshNetwork);
            groupBoxNetworkPC.Controls.Add(buttonNetwork);
            groupBoxNetworkPC.Controls.Add(pictureBoxPC);
            groupBoxNetworkPC.Controls.Add(comboBoxEthernet);
            groupBoxNetworkPC.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxNetworkPC.Location = new Point(10, 96);
            groupBoxNetworkPC.Name = "groupBoxNetworkPC";
            groupBoxNetworkPC.Size = new Size(577, 79);
            groupBoxNetworkPC.TabIndex = 0;
            groupBoxNetworkPC.TabStop = false;
            groupBoxNetworkPC.Text = "Network PC";
            // 
            // buttonRefreshNetwork
            // 
            buttonRefreshNetwork.Location = new Point(342, 48);
            buttonRefreshNetwork.Name = "buttonRefreshNetwork";
            buttonRefreshNetwork.Size = new Size(227, 23);
            buttonRefreshNetwork.TabIndex = 4;
            buttonRefreshNetwork.Text = "Refresh";
            buttonRefreshNetwork.UseVisualStyleBackColor = true;
            buttonRefreshNetwork.Click += ButtonRefreshNetwork_Click;
            // 
            // buttonNetwork
            // 
            buttonNetwork.Location = new Point(100, 48);
            buttonNetwork.Name = "buttonNetwork";
            buttonNetwork.Size = new Size(225, 23);
            buttonNetwork.TabIndex = 2;
            buttonNetwork.Text = "Show";
            buttonNetwork.UseVisualStyleBackColor = true;
            buttonNetwork.Click += ButtonShowNetwork_Click;
            // 
            // pictureBoxPC
            // 
            pictureBoxPC.Image = Properties.Resources.smart_tv;
            pictureBoxPC.Location = new Point(8, 21);
            pictureBoxPC.Name = "pictureBoxPC";
            pictureBoxPC.Size = new Size(75, 52);
            pictureBoxPC.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPC.TabIndex = 23;
            pictureBoxPC.TabStop = false;
            // 
            // comboBoxEthernet
            // 
            comboBoxEthernet.BackColor = SystemColors.ControlLight;
            comboBoxEthernet.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEthernet.FormattingEnabled = true;
            comboBoxEthernet.Location = new Point(100, 21);
            comboBoxEthernet.Name = "comboBoxEthernet";
            comboBoxEthernet.Size = new Size(469, 21);
            comboBoxEthernet.TabIndex = 3;
            comboBoxEthernet.SelectedIndexChanged += ComboBoxEthernet_SelectedIndexChanged;
            // 
            // textBoxStage1
            // 
            textBoxStage1.Enabled = false;
            textBoxStage1.Location = new Point(6, 17);
            textBoxStage1.Name = "textBoxStage1";
            textBoxStage1.ReadOnly = true;
            textBoxStage1.Size = new Size(207, 22);
            textBoxStage1.TabIndex = 0;
            textBoxStage1.TabStop = false;
            // 
            // buttonBrowseStage1
            // 
            buttonBrowseStage1.Location = new Point(225, 17);
            buttonBrowseStage1.Name = "buttonBrowseStage1";
            buttonBrowseStage1.Size = new Size(139, 22);
            buttonBrowseStage1.TabIndex = 13;
            buttonBrowseStage1.Text = "Browse";
            buttonBrowseStage1.UseVisualStyleBackColor = true;
            buttonBrowseStage1.Click += ButtonBrowseStage1_Click;
            // 
            // groupBoxStage1
            // 
            groupBoxStage1.Controls.Add(textBoxStage1);
            groupBoxStage1.Controls.Add(buttonBrowseStage1);
            groupBoxStage1.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxStage1.Location = new Point(217, 181);
            groupBoxStage1.Name = "groupBoxStage1";
            groupBoxStage1.Size = new Size(370, 45);
            groupBoxStage1.TabIndex = 0;
            groupBoxStage1.TabStop = false;
            groupBoxStage1.Text = "Stage1";
            // 
            // groupBoxScript
            // 
            groupBoxScript.BackColor = SystemColors.MenuBar;
            groupBoxScript.Controls.Add(buttonBrowseOffsets);
            groupBoxScript.Controls.Add(textBoxOffsets);
            groupBoxScript.Controls.Add(pictureBox1);
            groupBoxScript.Controls.Add(textBoxScript);
            groupBoxScript.Controls.Add(buttonBrowsePPPwn);
            groupBoxScript.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxScript.Location = new Point(10, 4);
            groupBoxScript.Name = "groupBoxScript";
            groupBoxScript.Size = new Size(577, 86);
            groupBoxScript.TabIndex = 0;
            groupBoxScript.TabStop = false;
            groupBoxScript.Text = "Script";
            // 
            // buttonBrowseOffsets
            // 
            buttonBrowseOffsets.Location = new Point(98, 52);
            buttonBrowseOffsets.Name = "buttonBrowseOffsets";
            buttonBrowseOffsets.Size = new Size(101, 23);
            buttonBrowseOffsets.TabIndex = 26;
            buttonBrowseOffsets.Text = "Browse Offsets";
            buttonBrowseOffsets.UseVisualStyleBackColor = true;
            buttonBrowseOffsets.Click += ButtonBrowseOffsets_Click;
            // 
            // textBoxOffsets
            // 
            textBoxOffsets.BackColor = SystemColors.Control;
            textBoxOffsets.Enabled = false;
            textBoxOffsets.Location = new Point(211, 53);
            textBoxOffsets.Name = "textBoxOffsets";
            textBoxOffsets.ReadOnly = true;
            textBoxOffsets.Size = new Size(356, 22);
            textBoxOffsets.TabIndex = 25;
            textBoxOffsets.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.python;
            pictureBox1.Location = new Point(8, 21);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(75, 55);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 24;
            pictureBox1.TabStop = false;
            // 
            // textBoxScript
            // 
            textBoxScript.BackColor = SystemColors.Control;
            textBoxScript.Enabled = false;
            textBoxScript.Location = new Point(211, 21);
            textBoxScript.Name = "textBoxScript";
            textBoxScript.ReadOnly = true;
            textBoxScript.Size = new Size(356, 22);
            textBoxScript.TabIndex = 0;
            textBoxScript.TabStop = false;
            // 
            // buttonSaveAllScripts
            // 
            buttonSaveAllScripts.Enabled = false;
            buttonSaveAllScripts.Location = new Point(293, 283);
            buttonSaveAllScripts.Name = "buttonSaveAllScripts";
            buttonSaveAllScripts.Size = new Size(137, 23);
            buttonSaveAllScripts.TabIndex = 14;
            buttonSaveAllScripts.Text = "Save  All To";
            buttonSaveAllScripts.UseVisualStyleBackColor = true;
            buttonSaveAllScripts.Click += ButtonSaveAllScripts_Click;
            // 
            // pictureBoxPS4
            // 
            pictureBoxPS4.Image = Properties.Resources.playstation4pro;
            pictureBoxPS4.Location = new Point(8, 18);
            pictureBoxPS4.Name = "pictureBoxPS4";
            pictureBoxPS4.Size = new Size(75, 70);
            pictureBoxPS4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPS4.TabIndex = 22;
            pictureBoxPS4.TabStop = false;
            // 
            // groupBoxNetworkPS
            // 
            groupBoxNetworkPS.Controls.Add(comboBoxFirmware);
            groupBoxNetworkPS.Controls.Add(pictureBoxPS4);
            groupBoxNetworkPS.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxNetworkPS.Location = new Point(10, 181);
            groupBoxNetworkPS.Name = "groupBoxNetworkPS";
            groupBoxNetworkPS.Size = new Size(201, 96);
            groupBoxNetworkPS.TabIndex = 0;
            groupBoxNetworkPS.TabStop = false;
            groupBoxNetworkPS.Text = "Firmware Playstation";
            // 
            // comboBoxFirmware
            // 
            comboBoxFirmware.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFirmware.FormattingEnabled = true;
            comboBoxFirmware.Items.AddRange(new object[] { "11.00", "10.01", "9.00" });
            comboBoxFirmware.Location = new Point(100, 18);
            comboBoxFirmware.Name = "comboBoxFirmware";
            comboBoxFirmware.Size = new Size(95, 21);
            comboBoxFirmware.TabIndex = 23;
            comboBoxFirmware.SelectedIndexChanged += ComboBoxFirmware_SelectedIndexChanged;
            // 
            // groupBoxStage2
            // 
            groupBoxStage2.Controls.Add(textBoxStage2);
            groupBoxStage2.Controls.Add(buttonBrowseStage2);
            groupBoxStage2.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxStage2.Location = new Point(217, 232);
            groupBoxStage2.Name = "groupBoxStage2";
            groupBoxStage2.Size = new Size(370, 45);
            groupBoxStage2.TabIndex = 14;
            groupBoxStage2.TabStop = false;
            groupBoxStage2.Text = "Stage2";
            // 
            // textBoxStage2
            // 
            textBoxStage2.Enabled = false;
            textBoxStage2.Location = new Point(6, 17);
            textBoxStage2.Name = "textBoxStage2";
            textBoxStage2.ReadOnly = true;
            textBoxStage2.Size = new Size(207, 22);
            textBoxStage2.TabIndex = 0;
            textBoxStage2.TabStop = false;
            // 
            // buttonBrowseStage2
            // 
            buttonBrowseStage2.Location = new Point(225, 17);
            buttonBrowseStage2.Name = "buttonBrowseStage2";
            buttonBrowseStage2.Size = new Size(139, 22);
            buttonBrowseStage2.TabIndex = 13;
            buttonBrowseStage2.Text = "Browse";
            buttonBrowseStage2.UseVisualStyleBackColor = true;
            buttonBrowseStage2.Click += ButtonBrowseStage2_Click;
            // 
            // buttonSaveShell
            // 
            buttonSaveShell.Enabled = false;
            buttonSaveShell.Location = new Point(150, 283);
            buttonSaveShell.Name = "buttonSaveShell";
            buttonSaveShell.Size = new Size(137, 23);
            buttonSaveShell.TabIndex = 16;
            buttonSaveShell.Text = "Save  Batch";
            buttonSaveShell.UseVisualStyleBackColor = true;
            buttonSaveShell.Click += ButtonSaveShell_Click;
            // 
            // buttonSavePythons
            // 
            buttonSavePythons.Location = new Point(10, 283);
            buttonSavePythons.Name = "buttonSavePythons";
            buttonSavePythons.Size = new Size(134, 23);
            buttonSavePythons.TabIndex = 17;
            buttonSavePythons.Text = "Save  Pythons";
            buttonSavePythons.UseVisualStyleBackColor = true;
            buttonSavePythons.Click += ButtonSavePythons_Click;
            // 
            // FormExecute
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.MenuBar;
            ClientSize = new Size(594, 311);
            Controls.Add(buttonSavePythons);
            Controls.Add(buttonSaveShell);
            Controls.Add(groupBoxStage2);
            Controls.Add(groupBoxNetworkPS);
            Controls.Add(groupBoxScript);
            Controls.Add(buttonSaveAllScripts);
            Controls.Add(buttonStart);
            Controls.Add(groupBoxStage1);
            Controls.Add(groupBoxNetworkPC);
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormExecute";
            Text = "EZ PPPwn-v1.00 by DjPopol";
            Load += Form1_Load;
            groupBoxNetworkPC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC).EndInit();
            groupBoxStage1.ResumeLayout(false);
            groupBoxStage1.PerformLayout();
            groupBoxScript.ResumeLayout(false);
            groupBoxScript.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPS4).EndInit();
            groupBoxNetworkPS.ResumeLayout(false);
            groupBoxStage2.ResumeLayout(false);
            groupBoxStage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonBrowsePPPwn;
        private System.Windows.Forms.PictureBox pictureBoxPC;
        private System.Windows.Forms.GroupBox groupBoxNetworkPC;
        private System.Windows.Forms.TextBox textBoxStage1;
        private System.Windows.Forms.Button buttonBrowseStage1;
        private System.Windows.Forms.Button buttonRefreshNetwork;
        private System.Windows.Forms.Button buttonNetwork;
        private System.Windows.Forms.ComboBox comboBoxEthernet;
        private System.Windows.Forms.GroupBox groupBoxStage1;
        private System.Windows.Forms.GroupBox groupBoxScript;
        private System.Windows.Forms.TextBox textBoxScript;
        private System.Windows.Forms.Button buttonSaveAllScripts;
        private PictureBox pictureBoxPS4;
        private GroupBox groupBoxNetworkPS;
        private ComboBox comboBoxFirmware;
        private PictureBox pictureBox1;
        private GroupBox groupBoxStage2;
        private TextBox textBoxStage2;
        private Button buttonBrowseStage2;
        private TextBox textBoxOffsets;
        private Button buttonBrowseOffsets;
        private Button buttonSaveShell;
        private Button buttonSavePythons;
    }
}

