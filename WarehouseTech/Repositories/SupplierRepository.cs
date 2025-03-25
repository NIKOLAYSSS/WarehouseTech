using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseTech.Models;
using WarehouseTech.Repositories.Interfaces;

namespace WarehouseTech.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DatabaseConnection databaseConnection)
            : base(databaseConnection)
        {
        }

        // Метод, который возвращает поставщиков по продукту
        public async Task<IEnumerable<Supplier>> GetByProductAsync(int productId)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var query = @"
                    SELECT s.* 
                    FROM suppliers s
                    JOIN product_suppliers ps ON s.id = ps.supplier_id
                    WHERE ps.product_id = @product_id";

                return await connection.QueryAsync<Supplier>(query, new { product_id = productId });
            }
        }
    }
}

