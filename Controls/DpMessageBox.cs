namespace DpLib.Winform.Controls
{
    public partial class DpMessageBox : Form
    {
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

        public DpMessageBox(string message, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, bool withCheckBox = false, bool isChecked = false)
        {
            InitializeComponent();
            lblMessage.Text = message;
            Text = caption;
            SetButtons(buttons);
            SetIcon(icon);
            checkBox.Visible = withCheckBox;
            CheckBoxChecked = isChecked;
        }

        private void SetButtons(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    btnYes.Text = "OK";
                    btnYes.Visible = true;
                    btnNo.Visible = false;
                    btnCancel.Visible = false;
                    AcceptButton = btnYes;
                    break;
                case MessageBoxButtons.OKCancel:
                    btnYes.Text = "OK";
                    btnNo.Text = "Cancel";
                    btnCancel.Visible = false;
                    btnYes.Visible = true;
                    btnNo.Visible = true;
                    AcceptButton = btnYes;
                    CancelButton = btnNo;
                    break;
                case MessageBoxButtons.YesNo:
                    btnYes.Text = "Yes";
                    btnNo.Text = "No";
                    btnCancel.Visible = false;
                    btnYes.Visible = true;
                    btnNo.Visible = true;
                    AcceptButton = btnYes;
                    CancelButton = btnNo;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    btnYes.Text = "Yes";
                    btnNo.Text = "No";
                    btnCancel.Text = "Cancel";
                    btnYes.Visible = true;
                    btnNo.Visible = true;
                    btnCancel.Visible = true;
                    AcceptButton = btnYes;
                    CancelButton = btnCancel;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttons), buttons, null);
            }
        }

        private void SetIcon(MessageBoxIcon icon)
        {
            pictureBoxIcon.Image = icon switch
            {
                MessageBoxIcon.Information => SystemIcons.Information.ToBitmap(),
                MessageBoxIcon.Warning => SystemIcons.Warning.ToBitmap(),
                MessageBoxIcon.Error => SystemIcons.Error.ToBitmap(),
                MessageBoxIcon.Question => SystemIcons.Question.ToBitmap(),
                _ => null,
            };
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

        private void CustomMessageBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
    }
}
