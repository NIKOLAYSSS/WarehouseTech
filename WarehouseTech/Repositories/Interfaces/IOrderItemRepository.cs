using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;

namespace WarehouseTech.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddAsync(OrderItem item);
        Task<IEnumerable<OrderItem>> GetAllAsync();
        Task DeleteByOrderAsync(int orderId); // Добавленный метод
        Task<IEnumerable<OrderItem>> GetByProductAsync(int productId);
    }
}
