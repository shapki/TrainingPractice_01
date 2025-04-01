using Shapkin_Task_9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Shapkin_Task_9.Forms
{
    public partial class fmLogin : Form
    {
        private bool passwordVisible = false;

        /// <summary>
        /// PKGH Конструктор формы авторизации.
        /// </summary>
        public fmLogin()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true; // Шифруем пароль по умолчанию
        }

        /// <summary>
        /// PKGH Обработчик нажатия кнопки "ОК".  Осуществляет проверку логина и пароля.
        /// </summary>
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            string username = txtLogin.Text;
            string password = txtPassword.Text;
            try
            {   // загрузка всех пользователей из БД в список
                List<User> users = Program.context.Users.ToList();
                // попытка найти пользователя с указанным паролем и логином
                // если такого пользователя не будет обнаружено то переменная u будет равна null
                User u = users.FirstOrDefault(p => p.UserName == username && p.Password == password);
                if (u != null)
                {
                    // логин и пароль корректные, запускаем главную форму приложения
                    MainWindow mainWindow = new MainWindow(u.Role, u.UserName);
                    mainWindow.Owner = this;
                    this.Hide();
                    txtPassword.Clear();
                    mainWindow.Show();
                }
                else
                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// PKGH Обработчик нажатия кнопки "Отмена".  Закрывает форму.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// PKGH Обработчик события закрытия формы.  Предлагает подтверждение выхода из приложения.
        /// </summary>
        private void fmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult x = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Выйти", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (x == DialogResult.No)
                e.Cancel = true;
            else
            {
                e.Cancel = true;
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// PKGH Обработчик нажатия на PictureBox "Скрыть пароль".
        /// </summary>
        private void pictureBoxUnVisible_Click(object sender, EventArgs e)
        {
            ShowPasswordCharacters(false);
        }

        /// <summary>
        /// PKGH Обработчик нажатия на PictureBox "Показать пароль".
        /// </summary>
        private void pictureBoxVisible_Click(object sender, EventArgs e)
        {
            ShowPasswordCharacters(true);
        }

        /// <summary>
        /// PKGH Переключает видимость символов пароля в текстовом поле.
        /// </summary>
        /// <param name="val">True - скрыть пароль, False - показать.</param>
        private void ShowPasswordCharacters(bool val)
        {
            passwordVisible = !val;
            txtPassword.UseSystemPasswordChar = val;
            pictureBoxVisible.Visible = !val;
            pictureBoxUnVisible.Visible = val;
        }

        /// <summary>
        /// PKGH Обработчик события нажатия клавиши в поле логина.
        /// </summary>
        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            PressOkButton(sender, e);
        }

        /// <summary>
        /// PKGH Обработчик события нажатия клавиши в поле пароля.
        /// </summary>
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            PressOkButton(sender, e);
        }

        /// <summary>
        /// PKGH Обработчик события нажатия Enter.
        /// </summary>
        private void PressOkButton(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnOk_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
