using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;

namespace WarehouseTech.Repositories.Interfaces
{
    public interface IShipmentItemRepository
    {
        Task AddAsync(ShipmentItem item);
        Task<IEnumerable<ShipmentItem>> GetByProductAsync(int productId);
    }
}
