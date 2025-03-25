using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;

namespace WarehouseTech.Repositories.Interfaces
{
    public interface IShipmentRepository : IRepository<Shipment>
    {
        Task<IEnumerable<Shipment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Shipment>> GetBySupplierAsync(int supplierId);
    }

}
