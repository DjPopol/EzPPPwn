namespace DpLib.Winform.Controls
{
    partial class DpMessageBox
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DpMessageBox));
            lblMessage = new Label();
            btnYes = new Button();
            btnNo = new Button();
            btnCancel = new Button();
            checkBox = new CheckBox();
            pictureBoxIcon = new PictureBox();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.BackColor = Color.Transparent;
            lblMessage.ForeColor = Color.Black;
            lblMessage.Location = new Point(51, 0);
            lblMessage.Margin = new Padding(4, 0, 4, 0);
            lblMessage.MaximumSize = new Size(303, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(256, 189);
            lblMessage.TabIndex = 0;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnYes
            // 
            btnYes.DialogResult = DialogResult.Yes;
            btnYes.ForeColor = Color.DodgerBlue;
            btnYes.Location = new Point(39, 222);
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
            btnNo.ForeColor = Color.DodgerBlue;
            btnNo.Location = new Point(135, 222);
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
            btnCancel.ForeColor = Color.DodgerBlue;
            btnCancel.Location = new Point(231, 222);
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
            checkBox.Location = new Point(12, 207);
            checkBox.Margin = new Padding(4, 3, 4, 3);
            checkBox.Name = "checkBox";
            checkBox.Size = new Size(15, 14);
            checkBox.TabIndex = 4;
            checkBox.UseVisualStyleBackColor = true;
            // 
            // pictureBoxIcon
            // 
            pictureBoxIcon.Location = new Point(5, 3);
            pictureBoxIcon.Margin = new Padding(4, 3, 4, 3);
            pictureBoxIcon.Name = "pictureBoxIcon";
            pictureBoxIcon.Size = new Size(37, 37);
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxIcon.TabIndex = 5;
            pictureBoxIcon.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(lblMessage);
            panel1.Controls.Add(pictureBoxIcon);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(307, 189);
            panel1.TabIndex = 6;
            // 
            // DpMessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DodgerBlue;
            ClientSize = new Size(331, 261);
            Controls.Add(panel1);
            Controls.Add(checkBox);
            Controls.Add(btnCancel);
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DpMessageBox";
            Shown += DpMessageBox_Shown;
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).EndInit();
            panel1.ResumeLayout(false);
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
        private Panel panel1;
    }
}
