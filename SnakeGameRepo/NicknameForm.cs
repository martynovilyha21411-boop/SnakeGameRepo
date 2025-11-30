using System;
using System.Windows.Forms;

namespace snake
{
    public partial class NicknameForm : Form
    {
        public string Nickname { get; private set; }


        public NicknameForm()
        {
            InitializeComponent();
        }

        public void SetCurrentNickname(string currentNickname)
        {
            if (textBoxNickname != null && !string.IsNullOrEmpty(currentNickname))
            {
                textBoxNickname.Text = currentNickname;
                textBoxNickname.SelectAll(); // Выделяем весь текст для удобства редактирования
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateNickname())
            {
                Nickname = textBoxNickname.Text.Trim();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidateNickname()
        {
            if (string.IsNullOrWhiteSpace(textBoxNickname.Text))
            {
                MessageBox.Show("Пожалуйста, введите никнейм!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNickname.Focus();
                return false;
            }

            if (textBoxNickname.Text.Trim().Length < 2)
            {
                MessageBox.Show("Никнейм должен содержать минимум 2 символа!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNickname.Focus();
                return false;
            }

            return true;
        }



        // Запрещаем закрытие формы через крестик без валидного ника
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!ValidateNickname())
                {
                    e.Cancel = true; // Отменяем закрытие
                }
                else
                {
                    Nickname = textBoxNickname.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                }
            }
            base.OnFormClosing(e);
        }

        // Нажатие Enter в текстовом поле
        private void textBoxNickname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonOK_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}