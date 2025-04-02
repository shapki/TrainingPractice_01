using Shapkin_Task_10.Models;
using System;
using System.Windows.Forms;

namespace Shapkin_Task_10.Forms
{
    internal static class Program
    {
        public static JewelryModel context = new JewelryModel();
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!context.Database.Exists())
            {
                MessageBox.Show("Не удаётся установить соединение с базой данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Создаем и передаем экземпляры сервисов
            ItemsService itemsService = new ItemsService();
            Application.Run(new MainWindow(itemsService));
        }
    }
}
