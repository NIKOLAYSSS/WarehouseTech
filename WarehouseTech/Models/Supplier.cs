using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTech.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Contact_phone { get; set; }
        public string Contact_email { get; set; }
        public string Address { get; set; }
        public DateTime Created_at { get; set; }
    }

}
