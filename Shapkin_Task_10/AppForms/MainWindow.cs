using Shapkin_Task_10.AppData;
using Shapkin_Task_10.Classes;
using Shapkin_Task_10.Properties;
using System;
using System.Windows.Forms;

namespace Shapkin_Task_10.Forms
{
    /// <summary>
    /// PKGH Главное окно приложения.
    /// </summary>
    public partial class MainWindow : Form
    {
        private bool _isAdmin = false;
        private bool _isLoggedIn = false;
        private readonly IItemsService _itemsService;

        public MainWindow(IItemsService itemsService)
        {
            InitializeComponent();
            Icon = Resources.shopCart1;
            _itemsService = itemsService ?? throw new ArgumentNullException(nameof(itemsService));
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmItems itemsForm = new fmItems(_isAdmin, _itemsService);
            itemsForm.ShowDialog();
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isAdmin)
            {
                ISalesService salesService = new SalesService();
                fmSales salesForm = new fmSales(salesService);
                salesForm.ShowDialog();
            }
            else
                MessageBox.Show("У вас нет прав для просмотра информации о продажах.", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void contactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmContacts contactsForm = new fmContacts();
            contactsForm.ShowDialog();
        }

        private void authorisationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_isLoggedIn)
                ShowAuthorisationMenu();
            else
            {
                DialogResult result = MessageBox.Show("Вы действительно хотите выйти?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _isLoggedIn = false;
                    _isAdmin = false;
                    UpdateMenuVisibility();
                    ShowAuthorisationMenu();
                }
            }
        }

        /// <summary>
        /// PKGH Отображает окно авторизации.
        /// </summary>
        private void ShowAuthorisationMenu()
        {
            AuthenticationService authenticationService = new AuthenticationService();
            fmLogin loginForm = new fmLogin(authenticationService);

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                _isAdmin = loginForm.IsAdmin;
                _isLoggedIn = loginForm.IsLoggedIn;
                UpdateMenuVisibility();
            }
        }

        /// <summary>
        /// PKGH Обновляет видимость пунктов меню в зависимости от прав пользователя.
        /// </summary>
        private void UpdateMenuVisibility()
        {
            salesToolStripMenuItem.Visible = _isAdmin;
            authorisationToolStripMenuItem.Text = (_isLoggedIn ? "Вы " + (_isAdmin ? "Админ" : "Польз") + ". Выйти?" : "Авторизация");
        }
    }
}
