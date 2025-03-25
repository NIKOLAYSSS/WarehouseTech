using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTech.Models
{
    public class OrderItem
    {
        public int Order_id { get; set; }
        public int Product_id { get; set; }
        public int Quantity { get; set; }
        public decimal Unit_price { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
