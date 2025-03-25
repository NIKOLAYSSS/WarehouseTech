using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;

namespace WarehouseTech.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> LoginAsync(string username, string password);
        Task UpdateLastLoginAsync(int userId);
        Task<bool> ValidateCredentialsAsync(User user, string password);
        Task<int> RegisterAsync(string username, string password, int roleId);
    }
}
