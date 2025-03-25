using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;
using WarehouseTech.Repositories.Interfaces;
using WarehouseTech.Services;

namespace WarehouseTech.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public UserRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                return await connection.QueryAsync<Role>("SELECT * FROM roles");
            }
        }

        public async Task<IEnumerable<User>> GetAllWithRolesAsync()
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var query = @"
            SELECT 
                u.id AS Id,
                u.username AS Username,
                u.role_id AS Role_id,
                u.created_at AS CreatedAt,
                u.last_login AS LastLogin,
                r.id AS RoleId, 
                r.name AS Name  -- Исправлено: r.name AS Name (совпадает с свойством Role.Name)
            FROM users u
            LEFT JOIN roles r ON u.role_id = r.id";

                return await connection.QueryAsync<User, Role, User>(
                    query,
                    (user, role) =>
                    {
                        user.Role = role ?? new Role { Name = "Роль не найдена" };
                        return user;
                    },
                    splitOn: "RoleId" // Указывает, где начинается маппинг Role
                );
            }
        }
        // Методы из IRepository<User>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var query = "SELECT * FROM users";
                return await connection.QueryAsync<User>(query);
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var query = "SELECT * FROM users WHERE id = @Id";
                return await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
            }
        }

        public async Task<int> AddAsync(User user)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                // Вставляем только столбцы, которые есть в таблице
                var query = @"
                INSERT INTO users (username, password_hash, role_id)
                VALUES (@Username, @Password_hash, @Role_id)
                RETURNING id";
                return await connection.QuerySingleAsync<int>(query, user);
            }
        }

        public async Task<bool> UpdateAsync(User user)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var query = @"
            UPDATE users
            SET 
                username = @Username, 
                password_hash = @Password_hash, 
                role_id = @Role_id,
                last_login = @LastLogin 
            WHERE id = @Id";

                var parameters = new
                {
                    user.Username,
                    user.Password_hash,
                    user.Role_id,
                    user.LastLogin,  // Передаем значение LastLogin
                    user.Id
                };

                var result = await connection.ExecuteAsync(query, parameters);
                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var query = "DELETE FROM users WHERE id = @Id";
                var result = await connection.ExecuteAsync(query, new { Id = id });
                return result > 0;
            }
        }

        // Методы из IUserRepository
        public async Task<User> GetByUsernameAsync(string username)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                Console.WriteLine($"Поиск пользователя: {username}");
                var query = "SELECT * FROM users WHERE username = @Username";
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { Username = username });

                if (user == null)
                    Console.WriteLine("Пользователь не найден в БД");
                else
                    Console.WriteLine($"Найден пользователь: {user.Username}, ID: {user.Id}");
                Console.WriteLine($"Отладка: username = {user?.Username}, password_hash = {user?.Password_hash}");

                return user;
            }
        }
        public async Task<int> CreateUserAsync(string username, string password, int roleId)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var passwordHash = AuthService.HashPassword(password); // Хэшируем пароль перед сохранением
                var query = @"
            INSERT INTO users (username, password_hash, role_id)
            VALUES (@Username, @Password_hash, @Role_id)
            RETURNING id";

                return await connection.QuerySingleAsync<int>(query, new
                {
                    Username = username,
                    PasswordHash = passwordHash,
                    RoleId = roleId
                });
            }
        }
        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                // Запрос к базе данных для получения роли по role_id
                var query = "SELECT * FROM roles WHERE id = @RoleId";
                return await connection.QueryFirstOrDefaultAsync<Role>(query, new { RoleId = roleId });
            }
               
        }
        public async Task<IEnumerable<User>> GetByRoleAsync(int roleId)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var query = @"
            SELECT 
                u.id AS Id,
                u.username AS Username,
                u.role_id AS Role_id,
                u.created_at AS CreatedAt,
                u.last_login AS LastLogin,
                r.id AS RoleId, 
                r.name AS Name 
            FROM users u
            LEFT JOIN roles r ON u.role_id = r.id
            WHERE @RoleId = 0 OR u.role_id = @RoleId"; // 0 = все роли

                return await connection.QueryAsync<User, Role, User>(
                    query,
                    (user, role) =>
                    {
                        user.Role = role ?? new Role { Name = "Роль не найдена" };
                        return user;
                    },
                    new { RoleId = roleId },
                    splitOn: "RoleId"
                );
            }
        }

    }



}
