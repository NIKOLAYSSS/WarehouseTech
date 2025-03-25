using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;

namespace WarehouseTech.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<InventoryReport>> GetInventoryReportAsync();
        Task<IEnumerable<ShipmentStatistics>> GetShipmentStatisticsAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<OrderStatistics>> GetOrderStatisticsAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<ProductMovementReport>> GetProductMovementReportAsync();
        Task<IEnumerable<PopularProductReport>> GetPopularProductsReportAsync();
    }

}
