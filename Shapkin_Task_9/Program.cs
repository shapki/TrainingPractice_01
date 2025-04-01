using Shapkin_Task_9.Forms;
using Shapkin_Task_9.Models;
using System;
using System.Windows.Forms;

namespace Shapkin_Task_9
{
    internal static class Program
    {
        public static ShopModel context = new ShopModel();
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "Каталог товаров - загрузка...";
            Console.WriteLine("Идет загрузка...");

            // Даем время на отображение текста
            System.Threading.Thread.Sleep(1000);

            // Пытаемся подключиться к базе данных
            if (!context.Database.Exists())
            {
                MessageBox.Show("Не удаётся установить соединение с базой данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Закрываем консольное окно
            IntPtr handle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            if (handle != IntPtr.Zero)
                NativeMethods.ShowWindow(handle, NativeMethods.SW_HIDE);

            // Запускаем форму fmLogin
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new fmLogin());
        }

        private static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            public const int SW_HIDE = 0;
        }
    }
}
