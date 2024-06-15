namespace DpLib.Winform.Controls
{
    partial class DpMessageBox : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DpMessageBox));
            lblMessage = new Label();
            btnYes = new Button();
            btnNo = new Button();
            btnCancel = new Button();
            checkBox = new CheckBox();
            pictureBoxIcon = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).BeginInit();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.ForeColor = Color.White;
            lblMessage.Location = new Point(70, 23);
            lblMessage.Margin = new Padding(4, 0, 4, 0);
            lblMessage.MaximumSize = new Size(303, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(0, 15);
            lblMessage.TabIndex = 0;
            // 
            // btnYes
            // 
            btnYes.DialogResult = DialogResult.Yes;
            btnYes.Location = new Point(19, 92);
            btnYes.Margin = new Padding(4, 3, 4, 3);
            btnYes.Name = "btnYes";
            btnYes.Size = new Size(88, 27);
            btnYes.TabIndex = 1;
            btnYes.Text = "Yes";
            btnYes.UseVisualStyleBackColor = true;
            btnYes.Click += BtnYes_Click;
            // 
            // btnNo
            // 
            btnNo.DialogResult = DialogResult.No;
            btnNo.Location = new Point(113, 92);
            btnNo.Margin = new Padding(4, 3, 4, 3);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(88, 27);
            btnNo.TabIndex = 2;
            btnNo.Text = "No";
            btnNo.UseVisualStyleBackColor = true;
            btnNo.Click += BtnNo_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(208, 92);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 27);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // checkBox
            // 
            checkBox.AutoSize = true;
            checkBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checkBox.ForeColor = Color.White;
            checkBox.Location = new Point(19, 65);
            checkBox.Margin = new Padding(4, 3, 4, 3);
            checkBox.Name = "checkBox";
            checkBox.Size = new Size(15, 14);
            checkBox.TabIndex = 4;
            checkBox.UseVisualStyleBackColor = true;
            // 
            // pictureBoxIcon
            // 
            pictureBoxIcon.Location = new Point(19, 18);
            pictureBoxIcon.Margin = new Padding(4, 3, 4, 3);
            pictureBoxIcon.Name = "pictureBoxIcon";
            pictureBoxIcon.Size = new Size(37, 37);
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxIcon.TabIndex = 5;
            pictureBoxIcon.TabStop = false;
            // 
            // CustomMessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DodgerBlue;
            ClientSize = new Size(331, 133);
            Controls.Add(pictureBoxIcon);
            Controls.Add(checkBox);
            Controls.Add(btnCancel);
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            Controls.Add(lblMessage);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomMessageBox";
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
    }
}
