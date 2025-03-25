using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTech.Models
{
    public class PopularProductReport
    {
        public string ProductName { get; set; } // Было Product_Name
        public int TotalOrders { get; set; }
        public int TotalSoldQuantity { get; set; }
        public int PopularityRank { get; set; }
    }
}
