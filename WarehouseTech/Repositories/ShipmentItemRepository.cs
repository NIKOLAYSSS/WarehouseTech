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
    public class ShipmentItemRepository : IShipmentItemRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public ShipmentItemRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<ShipmentItem>> GetByProductAsync(int productId)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<ShipmentItem>(
                    "SELECT * FROM shipment_items WHERE product_id = @ProductId",
                    new { ProductId = productId }
                );
            }
        }
        public async Task AddAsync(ShipmentItem item)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(
                    "INSERT INTO shipment_items (shipment_id, product_id, quantity, unit_price) " +
                    "VALUES (@shipment_id, @product_id, @Quantity, @unit_price)",
                    item
                );
            }
        }
    }
}
