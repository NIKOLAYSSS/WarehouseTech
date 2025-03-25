using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTech.Models
{
    public class OrderStatistics
    {
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public int TotalProductsTypes { get; set; }  // Количество уникальных товаров
        public int TotalQuantity { get; set; }       // Общее количество
        public decimal TotalAmount { get; set; }     // Сумма заказа
    }

}
