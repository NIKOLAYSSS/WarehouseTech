using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTech.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Customer_name { get; set; } = string.Empty;
        public string Customer_contact { get; set; } = string.Empty;
        public DateTime Order_date { get; set; }
        public string Status { get; set; } = "Ожидание";
        public int? Processed_by { get; set; }
        public decimal? Total_amount { get; set; }

        public User ProcessedByUser { get; set; } // Связь с пользователем
        public List<OrderItem> Items { get; set; } // Связь с элементами заказа
    }

}
