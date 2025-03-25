using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTech.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password_hash { get; set; } = string.Empty;
        public int Role_id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }

        public Role Role { get; set; } = new Role();
        public string RoleName => Role?.Name ?? "Роль не указана";
    }

}
