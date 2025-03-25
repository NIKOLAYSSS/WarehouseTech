using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;

namespace WarehouseTech.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetByRoleAsync(int roleId);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<IEnumerable<User>> GetAllWithRolesAsync();
        Task<int> CreateUserAsync(string username, string password, int roleId);
        Task<Role> GetRoleByIdAsync(int roleId);
    }

}
