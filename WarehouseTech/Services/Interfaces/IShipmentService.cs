using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;

namespace WarehouseTech.Services.Interfaces
{
    public interface IShipmentService
    {
        Task<int> AddShipmentAsync(Shipment shipment);
        Task<bool> UpdateShipmentAsync(Shipment shipment);
        Task<IEnumerable<Shipment>> GetAllShipmentsAsync();
        Task<Shipment> GetShipmentByIdAsync(int id);
        Task<IEnumerable<Shipment>> GetShipmentsBySupplierAsync(int supplierId);
        Task<IEnumerable<Shipment>> GetShipmentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Shipment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task DeleteAsync(int id);
        Task CreateShipmentWithItemsAsync(Shipment shipment, IEnumerable<ShipmentItem> items);
    }

}
