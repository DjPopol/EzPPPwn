using Dplib.Winform.Models;

namespace DpLib.Winform.Controls
{
    public partial class DpMessageBox : Form
    {
        const int MaxHeight = 600; // Maximum height for the form
        const int MarginHeight = 20; // Margin height for padding
        public string CheckBoxText
        {
            get { return checkBox.Text; }
            set { checkBox.Text = value; }
        }

        public bool CheckBoxChecked
        {
            get { return checkBox.Checked; }
            set { checkBox.Checked = value; }
        }
        MessageBoxButtons _buttons;
        public DpMessageBox(string message, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, bool withCheckBox = false, bool isChecked = false)
        {
            InitializeComponent();

            lblMessage.Text = message;
            Text = caption;
            _buttons = buttons;
            SetIcon(icon);
            CheckBoxChecked = isChecked;
            checkBox.Visible = withCheckBox;
        }
        private void DpMessageBox_Shown(object sender, EventArgs e)
        {
            AdjustLayout();
        }

        private void SetIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    pictureBoxIcon.Image = SystemIcons.Information.ToBitmap();
                    break;
                case MessageBoxIcon.Warning:
                    pictureBoxIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
                case MessageBoxIcon.Error:
                    pictureBoxIcon.Image = SystemIcons.Error.ToBitmap();
                    break;
                case MessageBoxIcon.Question:
                    pictureBoxIcon.Image = SystemIcons.Question.ToBitmap();
                    break;
                default:
                    pictureBoxIcon.Visible = false;
                    break;
            }
        }

        private void AdjustLayout()
        {
            int panelWidth = panel1.Width;
            int pictureBoxWidth = pictureBoxIcon.Visible ? pictureBoxIcon.Width + 10 : 0;

            lblMessage.MaximumSize = new Size(panelWidth - pictureBoxWidth - 20, 0);
            lblMessage.AutoSize = true;

            // Calculate required height
            int requiredHeight = lblMessage.Height + MarginHeight;

            if (requiredHeight > MaxHeight)
            {
                lblMessage.MaximumSize = new Size(lblMessage.MaximumSize.Width, MaxHeight - MarginHeight);
                lblMessage.AutoSize = false;
                lblMessage.Size = new Size(lblMessage.MaximumSize.Width, MaxHeight - MarginHeight);
                lblMessage.AutoEllipsis = true;
                requiredHeight = MaxHeight;
            }

            // Adjust panel and form size
            panel1.Size = new Size(panelWidth, requiredHeight);
            checkBox.Visible = checkBox.Text != string.Empty;
            // Adjust checkbox visibility and position
            if (checkBox.Visible)
            {
                checkBox.Location = new Point(checkBox.Location.X, panel1.Bottom + 10);
            }

            // Calculate button position
            int buttonY = panel1.Bottom + 5;
            if (checkBox.Visible)
            {
                buttonY = Math.Max(buttonY, checkBox.Bottom + 5);
            }
            // Position buttons
            int spaceTotal = ClientSize.Width;
            int space;
            switch (_buttons)
            {
                case MessageBoxButtons.OK:
                    btnYes.Visible = true;
                    btnYes.Text = "Ok";
                    btnNo.Visible = false;
                    btnCancel.Visible = false;
                    space = (spaceTotal - btnYes.Width) / 2;
                    btnYes.Location = new Point(space, buttonY);
                    break;
                case MessageBoxButtons.OKCancel:
                    btnYes.Visible = true;
                    btnYes.Text = "Yes";
                    btnNo.Visible = false;
                    btnCancel.Visible = true;
                    space = (spaceTotal - (btnYes.Width + btnCancel.Width + 5)) / 2;
                    btnYes.Location = new Point(space, buttonY);
                    btnCancel.Location = new Point(btnYes.Location.X + btnYes.Width + 5, buttonY);

                    break;
                case MessageBoxButtons.YesNo:
                    btnYes.Visible = true;
                    btnYes.Text = "Yes";
                    btnNo.Visible = true;
                    btnCancel.Visible = false;
                    space = (spaceTotal - (btnYes.Width + btnCancel.Width + 5)) / 2;
                    btnYes.Location = new Point(space, buttonY);
                    btnNo.Location = new Point(btnYes.Location.X + btnYes.Width + 5, buttonY);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    btnYes.Visible = true;
                    btnYes.Text = "Yes";
                    btnNo.Visible = true;
                    btnCancel.Visible = true;
                    space = (spaceTotal - (btnYes.Width + btnCancel.Width)) / 2;
                    btnYes.Location = new Point(space, buttonY);
                    btnNo.Location = new Point(btnYes.Location.X + btnYes.Width + 5, buttonY);
                    btnCancel.Location = new Point(btnYes.Location.X + btnNo.Width + 5, buttonY);
                    break;
            }
            ClientSize = new Size(ClientSize.Width, panel1.Height + (checkBox.Visible ? checkBox.Height : 0) + btnYes.Height + (checkBox.Visible ? 40 : 30));
            Refresh();
        }

        private void BtnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void BtnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        public static DialogResult ShowDialog(string message, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            DpMessageBox messageBox = new(message, caption, buttons, icon);
            DialogResult result = messageBox.ShowDialog();
            return result;
        }
        public static DpMessageBoxResult ShowDialog(string message, string caption, string checkBoxText, bool isChecked, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            DpMessageBox messageBox = new(message, caption, buttons, icon, true, isChecked)
            {
                CheckBoxText = checkBoxText
            };
            DialogResult result = messageBox.ShowDialog();
            return new() { DialogResult = result, IsChecked = messageBox.CheckBoxChecked };
        }
    }
}
