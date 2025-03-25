using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTech.Models
{
    public class ShipmentItem
    {
        public int Id { get; set; } // Добавлено для маппинга
        public int Shipment_id { get; set; }
        public int Product_id { get; set; }
        public int Quantity { get; set; }
        public decimal Unit_price { get; set; }

        public Shipment Shipment { get; set; }
        public Product Product { get; set; }
    }

}
