using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTech.Models
{
    public class ProductMovementReport
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime MovementDate { get; set; }
        public int QuantityIn { get; set; }
        public int QuantityOut { get; set; }
    }

}
