using DpLib.Winform.Controls;

namespace EzPPPwn
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
            textBoxLog = new TextBox();
            buttonStart = new Button();
            buttonCancel = new Button();
            progressBar = new DpTextProgressBar();
            labelTimer = new Label();
            menuStrip1 = new MenuStrip();
            toolStripMenuItemConfig = new ToolStripMenuItem();
            showConsoleToolStripMenuItem = new ToolStripMenuItem();
            updateToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            labelStatus = new Label();
            labelFw = new Label();
            pictureBoxGitHub = new PictureBox();
            labelDjPopol = new Label();
            labelFirmware = new Label();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGitHub).BeginInit();
            SuspendLayout();
            // 
            // textBoxLog
            // 
            textBoxLog.BackColor = Color.Black;
            textBoxLog.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxLog.ForeColor = Color.White;
            textBoxLog.Location = new Point(7, 135);
            textBoxLog.Margin = new Padding(4, 3, 4, 3);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ReadOnly = true;
            textBoxLog.ScrollBars = ScrollBars.Both;
            textBoxLog.Size = new Size(460, 171);
            textBoxLog.TabIndex = 0;
            // 
            // buttonStart
            // 
            buttonStart.BackColor = Color.LightGray;
            buttonStart.FlatAppearance.BorderColor = Color.DimGray;
            buttonStart.FlatStyle = FlatStyle.Flat;
            buttonStart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonStart.ForeColor = Color.DodgerBlue;
            buttonStart.Location = new Point(187, 104);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(100, 25);
            buttonStart.TabIndex = 1;
            buttonStart.Text = "Start PPPwn";
            buttonStart.UseVisualStyleBackColor = false;
            buttonStart.Click += ButtonStart_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = Color.LightGray;
            buttonCancel.FlatAppearance.BorderColor = Color.DimGray;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.DodgerBlue;
            buttonCancel.Location = new Point(187, 104);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(100, 25);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Visible = false;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // progressBar
            // 
            progressBar.CustomText = "";
            progressBar.Location = new Point(7, 48);
            progressBar.Name = "progressBar";
            progressBar.ProgressColor = Color.LightGreen;
            progressBar.Round = 0;
            progressBar.Size = new Size(460, 25);
            progressBar.Step = 100;
            progressBar.TabIndex = 4;
            progressBar.TextColor = Color.Black;
            progressBar.TextFont = new Font("Times New Roman", 11F, FontStyle.Bold | FontStyle.Italic);
            progressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
            // 
            // labelTimer
            // 
            labelTimer.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTimer.ForeColor = Color.White;
            labelTimer.Location = new Point(315, 104);
            labelTimer.Name = "labelTimer";
            labelTimer.Size = new Size(152, 25);
            labelTimer.TabIndex = 5;
            labelTimer.TextAlign = ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.LightGray;
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItemConfig, showConsoleToolStripMenuItem, updateToolStripMenuItem, toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(474, 24);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItemConfig
            // 
            toolStripMenuItemConfig.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripMenuItemConfig.Image = Properties.Resources.config;
            toolStripMenuItemConfig.Name = "toolStripMenuItemConfig";
            toolStripMenuItemConfig.Size = new Size(28, 20);
            toolStripMenuItemConfig.TextImageRelation = TextImageRelation.Overlay;
            toolStripMenuItemConfig.Click += ConfigToolStripMenuItem_Click;
            // 
            // showConsoleToolStripMenuItem
            // 
            showConsoleToolStripMenuItem.CheckOnClick = true;
            showConsoleToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            showConsoleToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            showConsoleToolStripMenuItem.ForeColor = Color.DodgerBlue;
            showConsoleToolStripMenuItem.Name = "showConsoleToolStripMenuItem";
            showConsoleToolStripMenuItem.Size = new Size(96, 20);
            showConsoleToolStripMenuItem.Text = "Show Console";
            showConsoleToolStripMenuItem.TextImageRelation = TextImageRelation.Overlay;
            showConsoleToolStripMenuItem.Click += ShowConsoleToolStripMenuItem_Click;
            // 
            // updateToolStripMenuItem
            // 
            updateToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            updateToolStripMenuItem.ForeColor = Color.DodgerBlue;
            updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            updateToolStripMenuItem.Size = new Size(101, 20);
            updateToolStripMenuItem.Text = "Update PPPwn";
            updateToolStripMenuItem.Click += UpdatePPPwnToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(12, 20);
            // 
            // labelStatus
            // 
            labelStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelStatus.ForeColor = Color.White;
            labelStatus.Location = new Point(7, 76);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(460, 25);
            labelStatus.TabIndex = 8;
            labelStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelFw
            // 
            labelFw.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelFw.AutoSize = true;
            labelFw.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFw.ForeColor = Color.White;
            labelFw.Location = new Point(173, 24);
            labelFw.Margin = new Padding(0);
            labelFw.Name = "labelFw";
            labelFw.Size = new Size(89, 21);
            labelFw.TabIndex = 9;
            labelFw.Text = "Firmware :";
            labelFw.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBoxGitHub
            // 
            pictureBoxGitHub.Cursor = Cursors.Hand;
            pictureBoxGitHub.Image = Properties.Resources.github;
            pictureBoxGitHub.Location = new Point(7, 312);
            pictureBoxGitHub.Name = "pictureBoxGitHub";
            pictureBoxGitHub.Size = new Size(100, 25);
            pictureBoxGitHub.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxGitHub.TabIndex = 10;
            pictureBoxGitHub.TabStop = false;
            pictureBoxGitHub.Click += PictureBoxGitHub_Click;
            // 
            // labelDjPopol
            // 
            labelDjPopol.AutoSize = true;
            labelDjPopol.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelDjPopol.ForeColor = Color.White;
            labelDjPopol.Location = new Point(319, 312);
            labelDjPopol.Name = "labelDjPopol";
            labelDjPopol.Size = new Size(148, 25);
            labelDjPopol.TabIndex = 11;
            labelDjPopol.Text = "©2024 DjPopol";
            // 
            // labelFirmware
            // 
            labelFirmware.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelFirmware.AutoSize = true;
            labelFirmware.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFirmware.ForeColor = Color.Lime;
            labelFirmware.Location = new Point(262, 24);
            labelFirmware.Margin = new Padding(0);
            labelFirmware.Name = "labelFirmware";
            labelFirmware.Size = new Size(0, 21);
            labelFirmware.TabIndex = 12;
            labelFirmware.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DodgerBlue;
            ClientSize = new Size(474, 345);
            Controls.Add(labelFw);
            Controls.Add(labelFirmware);
            Controls.Add(labelDjPopol);
            Controls.Add(pictureBoxGitHub);
            Controls.Add(labelTimer);
            Controls.Add(textBoxLog);
            Controls.Add(progressBar);
            Controls.Add(menuStrip1);
            Controls.Add(buttonCancel);
            Controls.Add(buttonStart);
            Controls.Add(labelStatus);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "FormMain";
            FormClosing += FormMain_FormClosing;
            Shown += FormMain_Shown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGitHub).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLog;
        private Button buttonStart;
        private Button buttonCancel;
        private DpTextProgressBar progressBar;
        private Label labelTimer;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItemConfig;
        private Label labelStatus;
        private ToolStripMenuItem showConsoleToolStripMenuItem;
        private ToolStripMenuItem updateToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private Label labelFw;
        private PictureBox pictureBoxGitHub;
        private Label labelDjPopol;
        private Label labelFirmware;
    }
}