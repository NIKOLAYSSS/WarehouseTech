using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTech.Models
{
    public class ShipmentStatistics
    {
        public DateTime ShipmentDate { get; set; }
        public string SupplierName { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalCost { get; set; }
    }

}
