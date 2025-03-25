using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;

namespace WarehouseTech.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetBySupplierAsync(int supplierId);
        Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold);
        Task<IEnumerable<Product>> GetStockAsync(int productId); // Метод для получения всех продуктов
        Task<IEnumerable<Product>> GetByCategoryAsync(string category); // Метод для получения продуктов по категории
    }


}
