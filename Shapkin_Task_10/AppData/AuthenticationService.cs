using Shapkin_Task_10.Forms;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Shapkin_Task_10.AppData
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool ValidateUser(string username, string password, out bool isAdmin)
        {
            isAdmin = false;
            try
            {
                var user = Program.context.Users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                    if (VerifyPasswordHash(password, user.PasswordHash))
                    {
                        isAdmin = user.IsAdmin;
                        return true;
                    }

                return false; // Пользователь не найден или пароль неверный
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// PKGH Проверяет соответствие введенного пароля хешу из базы данных.
        /// </summary>
        /// <param name="password">Введенный пароль.</param>
        /// <param name="hash">Хеш пароля из базы данных.</param>
        /// <returns>True, если пароль соответствует хешу, иначе False.</returns>
        private bool VerifyPasswordHash(string password, string hash)
        {
            // Преобразование введенного пароля в SHA256 хеш
            string passwordHash = ComputeSha256Hash(password);

            // Сравнение вычисленного хеша с хешем из базы данных
            return string.Equals(passwordHash, hash, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// PKGH Вычисляет SHA256 хеш из строки.
        /// </summary>
        /// <param name="rawData">Строка для хеширования.</param>
        /// <returns>SHA256 хеш строки.</returns>
        private string ComputeSha256Hash(string rawData)
        {
            // Создание SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Вычисление хеша из массива байтов входных данных
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Преобразование массива байтов в строку
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
