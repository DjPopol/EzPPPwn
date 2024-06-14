using EzPPPwn.Controls;

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
            progressBar = new DP_TextProgressBar();
            labelTimer = new Label();
            menuStrip1 = new MenuStrip();
            toolStripMenuItemConfig = new ToolStripMenuItem();
            showConsoleToolStripMenuItem = new ToolStripMenuItem();
            updateToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            labelStatus = new Label();
            labelFw = new Label();
            toolStripMenuItemGithub = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxLog
            // 
            textBoxLog.BackColor = Color.Black;
            textBoxLog.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxLog.ForeColor = Color.White;
            textBoxLog.Location = new Point(11, 133);
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
            buttonStart.Location = new Point(189, 102);
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
            buttonCancel.Location = new Point(189, 102);
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
            progressBar.Location = new Point(12, 48);
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
            labelTimer.Location = new Point(319, 102);
            labelTimer.Name = "labelTimer";
            labelTimer.Size = new Size(152, 25);
            labelTimer.TabIndex = 5;
            labelTimer.TextAlign = ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.LightGray;
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItemConfig, showConsoleToolStripMenuItem, updateToolStripMenuItem, toolStripMenuItem1, toolStripMenuItemGithub });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(484, 24);
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
            updateToolStripMenuItem.Visible = false;
            updateToolStripMenuItem.Click += UpdateToolStripMenuItem_Click;
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
            labelStatus.Location = new Point(11, 76);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(460, 25);
            labelStatus.TabIndex = 8;
            labelStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelFw
            // 
            labelFw.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFw.ForeColor = Color.White;
            labelFw.Location = new Point(12, 24);
            labelFw.Name = "labelFw";
            labelFw.Size = new Size(460, 21);
            labelFw.TabIndex = 9;
            labelFw.Text = "Firmware :";
            labelFw.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // toolStripMenuItemGithub
            // 
            toolStripMenuItemGithub.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripMenuItemGithub.ForeColor = Color.DodgerBlue;
            toolStripMenuItemGithub.Name = "toolStripMenuItemGithub";
            toolStripMenuItemGithub.Size = new Size(105, 20);
            toolStripMenuItemGithub.Text = "DjPopol GitHub";
            toolStripMenuItemGithub.TextImageRelation = TextImageRelation.Overlay;
            toolStripMenuItemGithub.Click += ToolStripMenuItemGithub_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DodgerBlue;
            ClientSize = new Size(484, 131);
            Controls.Add(labelStatus);
            Controls.Add(labelTimer);
            Controls.Add(textBoxLog);
            Controls.Add(progressBar);
            Controls.Add(menuStrip1);
            Controls.Add(buttonCancel);
            Controls.Add(labelFw);
            Controls.Add(buttonStart);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLog;
        private Button buttonStart;
        private Button buttonCancel;
        private DP_TextProgressBar progressBar;
        private Label labelTimer;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItemConfig;
        private Label labelStatus;
        private ToolStripMenuItem showConsoleToolStripMenuItem;
        private ToolStripMenuItem updateToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private Label labelFw;
        private ToolStripMenuItem toolStripMenuItemGithub;
    }
}