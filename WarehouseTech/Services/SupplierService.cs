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
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _supplierRepository.GetAllAsync();
        }

        public async Task<Supplier> GetByIdAsync(int id)
        {
            return await _supplierRepository.GetByIdAsync(id);
        }

        public async Task<int> AddAsync(Supplier supplier)
        {
            // Добавляем дату создания
            supplier.Created_at = DateTime.UtcNow;
            return await _supplierRepository.AddAsync(supplier);
        }

        public async Task<bool> UpdateAsync(Supplier supplier)
        {
            return await _supplierRepository.UpdateAsync(supplier);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _supplierRepository.DeleteAsync(id);
        }
    }
}
