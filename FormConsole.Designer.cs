namespace Ez_PPPwn
{
    partial class FormConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConsole));
            textBoxLog = new TextBox();
            buttonSaveLog = new Button();
            SuspendLayout();
            // 
            // textBoxLog
            // 
            textBoxLog.BackColor = Color.Black;
            textBoxLog.ForeColor = Color.White;
            textBoxLog.Location = new Point(14, 14);
            textBoxLog.Margin = new Padding(4, 3, 4, 3);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ReadOnly = true;
            textBoxLog.ScrollBars = ScrollBars.Both;
            textBoxLog.Size = new Size(664, 469);
            textBoxLog.TabIndex = 0;
            // 
            // buttonSaveLog
            // 
            buttonSaveLog.Enabled = false;
            buttonSaveLog.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonSaveLog.Location = new Point(587, 489);
            buttonSaveLog.Name = "buttonSaveLog";
            buttonSaveLog.Size = new Size(91, 23);
            buttonSaveLog.TabIndex = 1;
            buttonSaveLog.Text = "Save Log As";
            buttonSaveLog.UseVisualStyleBackColor = true;
            buttonSaveLog.Click += ButtonSaveLog_Click;
            // 
            // FormConsole
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(692, 524);
            Controls.Add(buttonSaveLog);
            Controls.Add(textBoxLog);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "FormConsole";
            Text = "Console  EZ PPPwn-v1.00 by DjPopol ";
            Load += FormConsole_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLog;
        private Button buttonSaveLog;
    }
}