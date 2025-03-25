using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;
using WarehouseTech.Repositories.Interfaces;

namespace WarehouseTech.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseConnection databaseConnection)
            : base(databaseConnection)
        {
        }

        // Реализация метода для получения продуктов по поставщику
        public async Task<IEnumerable<Product>> GetBySupplierAsync(int supplierId)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                string query = @"
            SELECT p.* 
            FROM products p
            INNER JOIN supplier_products sp ON p.id = sp.product_id
            WHERE sp.supplier_id = @supplier_id";

                return await connection.QueryAsync<Product>(query, new { supplier_id = supplierId });
            }
        }

        // Реализация метода для получения продуктов с низким запасом
        public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                string query = "SELECT * FROM products WHERE quantity < @Threshold";
                return await connection.QueryAsync<Product>(query, new { Threshold = threshold });
            }
        }

        // Реализация метода для получения всех продуктов (или определенной информации о запасах)

        public async Task<IEnumerable<Product>> GetStockAsync(int productId)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                string query = "SELECT * FROM products WHERE id = @ProductId";
                return await connection.QueryAsync<Product>(query, new { ProductId = productId });
            }
        }

        // Реализация метода для получения продуктов по категории
        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                string query = "SELECT * FROM products WHERE category = @Category";
                return await connection.QueryAsync<Product>(query, new { Category = category });
            }
        }
    }


}
