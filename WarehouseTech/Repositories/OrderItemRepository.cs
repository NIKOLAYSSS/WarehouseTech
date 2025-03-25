using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WarehouseTech.Models;
using WarehouseTech.Repositories.Interfaces;

namespace WarehouseTech.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public OrderItemRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task DeleteByOrderAsync(int orderId)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(
                    "DELETE FROM order_items WHERE order_id = @OrderId",
                    new { OrderId = orderId }
                );
            }
        }
        public async Task<IEnumerable<OrderItem>> GetByProductAsync(int productId)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<OrderItem>(
                    "SELECT * FROM order_items WHERE product_id = @ProductId",
                    new { ProductId = productId }
                );
            }
        }
        public async Task AddAsync(OrderItem item)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(@"
                    INSERT INTO order_items 
                        (order_id, product_id, quantity, unit_price)
                    VALUES 
                        (@Order_Id, @Product_Id, @Quantity, @Unit_Price)",
                    item
                );
            }
        }
        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<OrderItem>(
                    "SELECT * FROM order_items");
            }
        }
    }
}