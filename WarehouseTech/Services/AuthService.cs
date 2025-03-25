using WarehouseTech.Models;
using WarehouseTech.Repositories;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System;
using WarehouseTech.Repositories.Interfaces;
using WarehouseTech.Services.Interfaces;

namespace WarehouseTech.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || !await ValidateCredentialsAsync(user, password))
                return null;
            user.Role = await _userRepository.GetRoleByIdAsync(user.Role_id);
            await UpdateLastLoginAsync(user.Id);
            return user;
        }

        public async Task UpdateLastLoginAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            user.LastLogin = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> ValidateCredentialsAsync(User user, string password)
        {
            return VerifyPassword(password, user.Password_hash);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            Console.WriteLine($"Проверяем пароль: {password}");
            Console.WriteLine($"Хэш из БД: {storedHash}");

            var parts = storedHash.Split('.');
            if (parts.Length != 2)
            {
                Console.WriteLine("Ошибка: хэш пароля имеет неверный формат.");
                return false;
            }

            var salt = Convert.FromBase64String(parts[0]);
            var storedHashBytes = Convert.FromBase64String(parts[1]);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, 600000, HashAlgorithmName.SHA256))
            {
                var computedHash = deriveBytes.GetBytes(32);
                Console.WriteLine($"Вычисленный хэш: {Convert.ToBase64String(computedHash)}");

                bool isValid = computedHash.SequenceEqual(storedHashBytes);
                Console.WriteLine($"Результат сравнения: {isValid}");

                return isValid;
            }
        }



        public async Task<int> RegisterAsync(string username, string password, int roleId)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(username);
            if (existingUser != null)
            {
                throw new Exception("Пользователь с таким именем уже существует.");
            }

            return await _userRepository.CreateUserAsync(username, password, roleId);
        }

        public static string HashPassword(string password)
{
    // Применяем то же количество итераций, что и в проверке пароля
    using (var deriveBytes = new Rfc2898DeriveBytes(password, 16, 600000, HashAlgorithmName.SHA256)) // Используем 600000, как в VerifyPassword
    {
        byte[] salt = deriveBytes.Salt;
        byte[] hash = deriveBytes.GetBytes(32);

        string result = $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        Console.WriteLine($"HashPassword вернул: {result}");
        return result;
    }
}
    }
}