using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService
{
    public static class PasswordUtils
    {
        private const string SecretKey = "5PH2L0BVO7CdJVXoxP5bBVjN";

        public static (string passwordSalt, string PasswordHash) CreatePasswordHash(string password)
        {
            //Генерируем соль (генерация случайной последовательности байт)
            byte[] buffer = new byte[16];
            RNGCryptoServiceProvider secureRandom = new RNGCryptoServiceProvider();
            secureRandom.GetBytes(buffer);

            //Создаем хэш
            string passwordSalt = Convert.ToBase64String(buffer);
            string passwordHash = GetPasswordHash(password, passwordSalt);

            return (passwordSalt, passwordHash);
        }

        public static bool VerifyPassword(string password, string passwordSalt, string passwordHash)
        {
            return GetPasswordHash(password, passwordSalt) == passwordHash;
        }

        public static string GetPasswordHash(string password, string passwordSalt)
        {
            //Подготавливаем строку пароля
            password = $"{password}~{passwordSalt}~{SecretKey}";
            byte[] buffer = Encoding.UTF8.GetBytes(password);

            //Вычисляем хэш
            SHA512 sha512 = new SHA512Managed();
            byte[] passwordHash = sha512.ComputeHash(buffer);

            return Convert.ToBase64String(passwordHash);
        }
    }
}
