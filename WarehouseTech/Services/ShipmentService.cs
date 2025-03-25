using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;
using WarehouseTech.Repositories.Interfaces;
using WarehouseTech.Services.Interfaces;

namespace WarehouseTech.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IShipmentItemRepository _shipmentItemRepository;

        public ShipmentService(IShipmentRepository shipmentRepository, IShipmentItemRepository shipmentItemRepository)
        {
            _shipmentRepository = shipmentRepository;
            _shipmentItemRepository = shipmentItemRepository;
        }
        public async Task DeleteAsync(int id)
        {
            await _shipmentRepository.DeleteAsync(id);
        }

        public async Task CreateShipmentWithItemsAsync(Shipment shipment, IEnumerable<ShipmentItem> items)
        {
            shipment.Total_cost = items.Sum(i => i.Quantity * i.Unit_price); // Расчет суммы
            int shipmentId = await _shipmentRepository.AddAsync(shipment);

            foreach (var item in items)
            {
                item.Shipment_id = shipmentId;
                await _shipmentItemRepository.AddAsync(item);
            }
        }
        public async Task<IEnumerable<Shipment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _shipmentRepository.GetByDateRangeAsync(startDate, endDate);
        }
        public async Task<int> AddShipmentAsync(Shipment shipment)
        {
            // Логика добавления новой поставки
            return await _shipmentRepository.AddAsync(shipment);
        }

        public async Task<bool> UpdateShipmentAsync(Shipment shipment)
        {
            // Логика обновления информации о поставке
            return await _shipmentRepository.UpdateAsync(shipment);
        }

        public async Task<IEnumerable<Shipment>> GetAllShipmentsAsync()
        {
            // Логика получения всех поставок
            return await _shipmentRepository.GetAllAsync();
        }

        public async Task<Shipment> GetShipmentByIdAsync(int id)
        {
            // Логика получения поставки по id
            return await _shipmentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Shipment>> GetShipmentsBySupplierAsync(int supplierId)
        {
            // Логика получения поставок по поставщику
            return await _shipmentRepository.GetBySupplierAsync(supplierId);
        }

        public async Task<IEnumerable<Shipment>> GetShipmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            // Логика получения поставок по диапазону дат
            return await _shipmentRepository.GetByDateRangeAsync(startDate, endDate);
        }
    }

}
