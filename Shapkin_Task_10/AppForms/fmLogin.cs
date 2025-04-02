using Shapkin_Task_10.AppData;
using Shapkin_Task_10.Properties;
using System;
using System.Windows.Forms;

namespace Shapkin_Task_10.Forms
{
    /// <summary>
    /// PKGH Форма для авторизации пользователя.
    /// </summary>
    public partial class fmLogin : Form
    {
        private readonly IAuthenticationService _authenticationService;

        public bool IsAdmin { get; private set; }
        public bool IsLoggedIn { get; private set; }

        public fmLogin(IAuthenticationService authenticationService)
        {
            InitializeComponent();
            Icon = Resources.shopCart1;
            IsAdmin = false;
            IsLoggedIn = false;
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            if (_authenticationService.ValidateUser(username, password, out bool isAdmin))
            {
                IsAdmin = isAdmin;
                DialogResult = DialogResult.OK;
                IsLoggedIn = true;
                Close();
            }
            else
                MessageBox.Show("Неверный логин или пароль.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
