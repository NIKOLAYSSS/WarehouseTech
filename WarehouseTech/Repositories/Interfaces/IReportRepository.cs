using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;

namespace WarehouseTech.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<InventoryReport>> GetInventoryReportAsync();
        Task<IEnumerable<ShipmentStatistics>> GetShipmentStatisticsAsync();
        Task<IEnumerable<OrderStatistics>> GetOrderStatisticsAsync();
        Task<IEnumerable<ProductMovementReport>> GetProductMovementReportAsync();
    }

}
