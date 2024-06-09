using EzPPPwn.Controls;

namespace EzPPPwn
{
    partial class FormInstallRequired
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInstallRequired));
            textBoxConsole = new TextBox();
            progressBarMain = new DP_TextProgressBar();
            progressBarCurrent = new DP_TextProgressBar();
            buttonCancel = new Button();
            menuStrip1 = new MenuStrip();
            showConsoleToolStripMenuItem = new ToolStripMenuItem();
            labelCurrentStatus = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxConsole
            // 
            textBoxConsole.BackColor = Color.Black;
            textBoxConsole.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxConsole.ForeColor = Color.White;
            textBoxConsole.Location = new Point(12, 149);
            textBoxConsole.Multiline = true;
            textBoxConsole.Name = "textBoxConsole";
            textBoxConsole.ReadOnly = true;
            textBoxConsole.ScrollBars = ScrollBars.Both;
            textBoxConsole.Size = new Size(460, 140);
            textBoxConsole.TabIndex = 36;
            // 
            // progressBarMain
            // 
            progressBarMain.CustomText = "";
            progressBarMain.Location = new Point(12, 31);
            progressBarMain.Name = "progressBarMain";
            progressBarMain.ProgressColor = Color.LightGreen;
            progressBarMain.Round = 0;
            progressBarMain.Size = new Size(460, 25);
            progressBarMain.TabIndex = 33;
            progressBarMain.Tag = "";
            progressBarMain.TextColor = Color.Black;
            progressBarMain.TextFont = new Font("Times New Roman", 11F, FontStyle.Bold | FontStyle.Italic);
            progressBarMain.VisualMode = ProgressBarDisplayMode.TextAndCurrProgress;
            // 
            // progressBarCurrent
            // 
            progressBarCurrent.CustomText = "";
            progressBarCurrent.Location = new Point(12, 87);
            progressBarCurrent.Name = "progressBarCurrent";
            progressBarCurrent.ProgressColor = Color.LightGreen;
            progressBarCurrent.Round = 0;
            progressBarCurrent.Size = new Size(460, 25);
            progressBarCurrent.TabIndex = 34;
            progressBarCurrent.Tag = "";
            progressBarCurrent.TextColor = Color.Black;
            progressBarCurrent.TextFont = new Font("Times New Roman", 11F, FontStyle.Bold | FontStyle.Italic);
            progressBarCurrent.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = Color.LightGray;
            buttonCancel.FlatAppearance.BorderColor = Color.DimGray;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.DodgerBlue;
            buttonCancel.Location = new Point(196, 118);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(100, 25);
            buttonCancel.TabIndex = 35;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.LightGray;
            menuStrip1.Items.AddRange(new ToolStripItem[] { showConsoleToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(484, 24);
            menuStrip1.TabIndex = 37;
            menuStrip1.Text = "menuStrip1";
            // 
            // showConsoleToolStripMenuItem
            // 
            showConsoleToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            showConsoleToolStripMenuItem.ForeColor = Color.DodgerBlue;
            showConsoleToolStripMenuItem.Name = "showConsoleToolStripMenuItem";
            showConsoleToolStripMenuItem.Size = new Size(96, 20);
            showConsoleToolStripMenuItem.Text = "Show Console";
            showConsoleToolStripMenuItem.Click += ShowConsoleToolStripMenuItem_Click;
            // 
            // labelCurrentStatus
            // 
            labelCurrentStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCurrentStatus.ForeColor = Color.White;
            labelCurrentStatus.Location = new Point(12, 59);
            labelCurrentStatus.Name = "labelCurrentStatus";
            labelCurrentStatus.Size = new Size(460, 25);
            labelCurrentStatus.TabIndex = 39;
            labelCurrentStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormInstallRequired
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DodgerBlue;
            ClientSize = new Size(484, 301);
            Controls.Add(labelCurrentStatus);
            Controls.Add(textBoxConsole);
            Controls.Add(progressBarMain);
            Controls.Add(progressBarCurrent);
            Controls.Add(buttonCancel);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "FormInstallRequired";
            Text = "Ez PPPwn v1.10 by DjPopol";
            Shown += FormCheckRequired_Shown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxConsole;
        private DP_TextProgressBar progressBarMain;
        private DP_TextProgressBar progressBarCurrent;
        private Button buttonCancel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem showConsoleToolStripMenuItem;
        private Label labelCurrentStatus;
    }
}