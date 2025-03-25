using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTech.Models
{
    public class InventoryReport
    {
        public string ProductName { get; set; }
        public decimal CurrentPrice { get; set; }
        public int CurrentQuantity { get; set; }
        public int TotalSupplied { get; set; }
        public int TotalSold { get; set; }
    }

}
