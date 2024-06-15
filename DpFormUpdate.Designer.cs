namespace DpLib.Winform
{
    partial class DpFormUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DpFormUpdate));
            progressBarMain = new Controls.DpTextProgressBar();
            progressBarCurrent = new Controls.DpTextProgressBar();
            labelCurrentStatus = new Label();
            buttonCancel = new Button();
            textBoxConsole = new TextBox();
            menuStrip1 = new MenuStrip();
            showConsoleToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // progressBarMain
            // 
            progressBarMain.CustomText = "";
            progressBarMain.Location = new Point(12, 33);
            progressBarMain.Name = "progressBarMain";
            progressBarMain.ProgressColor = Color.LightGreen;
            progressBarMain.Round = 2;
            progressBarMain.Size = new Size(460, 25);
            progressBarMain.TabIndex = 0;
            progressBarMain.TextColor = Color.Black;
            progressBarMain.TextFont = new Font("Times New Roman", 11F, FontStyle.Bold | FontStyle.Italic);
            progressBarMain.VisualMode = Winform.Controls.ProgressBarDisplayMode.TextAndCurrProgress;
            // 
            // progressBarCurrent
            // 
            progressBarCurrent.CustomText = "";
            progressBarCurrent.Location = new Point(12, 87);
            progressBarCurrent.Name = "progressBarCurrent";
            progressBarCurrent.ProgressColor = Color.LightGreen;
            progressBarCurrent.Round = 2;
            progressBarCurrent.Size = new Size(460, 25);
            progressBarCurrent.TabIndex = 1;
            progressBarCurrent.TextColor = Color.Black;
            progressBarCurrent.TextFont = new Font("Times New Roman", 11F, FontStyle.Bold | FontStyle.Italic);
            progressBarCurrent.VisualMode = Winform.Controls.ProgressBarDisplayMode.TextAndPercentage;
            // 
            // labelCurrentStatus
            // 
            labelCurrentStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCurrentStatus.ForeColor = Color.White;
            labelCurrentStatus.Location = new Point(12, 59);
            labelCurrentStatus.Name = "labelCurrentStatus";
            labelCurrentStatus.Size = new Size(460, 25);
            labelCurrentStatus.TabIndex = 2;
            labelCurrentStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = Color.LightGray;
            buttonCancel.FlatAppearance.BorderColor = Color.DimGray;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.DodgerBlue;
            buttonCancel.Location = new Point(192, 118);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(101, 25);
            buttonCancel.TabIndex = 36;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // textBoxConsole
            // 
            textBoxConsole.BackColor = Color.Black;
            textBoxConsole.ForeColor = Color.White;
            textBoxConsole.Location = new Point(12, 149);
            textBoxConsole.Multiline = true;
            textBoxConsole.Name = "textBoxConsole";
            textBoxConsole.Size = new Size(460, 140);
            textBoxConsole.TabIndex = 37;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { showConsoleToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(484, 24);
            menuStrip1.TabIndex = 38;
            menuStrip1.Text = "menuStrip1";
            // 
            // showConsoleToolStripMenuItem
            // 
            showConsoleToolStripMenuItem.BackColor = Color.Transparent;
            showConsoleToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            showConsoleToolStripMenuItem.ForeColor = Color.DodgerBlue;
            showConsoleToolStripMenuItem.Name = "showConsoleToolStripMenuItem";
            showConsoleToolStripMenuItem.Size = new Size(93, 20);
            showConsoleToolStripMenuItem.Text = "ShowConsole";
            showConsoleToolStripMenuItem.Click += ShowConsoleToolStripMenuItem_Click;
            // 
            // DpFormUpdate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DodgerBlue;
            ClientSize = new Size(484, 301);
            Controls.Add(textBoxConsole);
            Controls.Add(buttonCancel);
            Controls.Add(labelCurrentStatus);
            Controls.Add(progressBarCurrent);
            Controls.Add(progressBarMain);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DpFormUpdate";
            Text = "Update";
            Shown += DpFormUpdate_Shown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DpLib.Winform.Controls.DpTextProgressBar progressBarMain;
        private DpLib.Winform.Controls.DpTextProgressBar progressBarCurrent;
        private Label labelCurrentStatus;
        private Button buttonCancel;
        private TextBox textBoxConsole;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem showConsoleToolStripMenuItem;
    }
}