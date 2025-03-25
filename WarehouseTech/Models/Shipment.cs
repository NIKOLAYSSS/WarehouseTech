using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTech.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public int Supplier_id { get; set; }
        public DateTime Shipment_date { get; set; }
        public int? Received_by { get; set; }
        public decimal? Total_cost { get; set; }

        public Supplier Supplier { get; set; } // Связь с поставщиком
        public User Receiver { get; set; } // Связь с пользователем

        public List<ShipmentItem> Items { get; set; } // Связь с элементами поставки
        public string SupplierName => Supplier?.Name;
    }

}
