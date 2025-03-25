using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WarehouseTech.Models;
using WarehouseTech.Repositories.Interfaces;

namespace WarehouseTech.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public OrderRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Order>("SELECT * FROM orders");
            }
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Order>(
                    "SELECT * FROM orders WHERE id = @Id",
                    new { Id = id }
                );
            }
        }

        public async Task<int> AddAsync(Order order)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QuerySingleAsync<int>(@"
            INSERT INTO orders 
                (customer_name, customer_contact, order_date, status, processed_by, total_amount)
            VALUES 
                (@Customer_name, @Customer_contact, @Order_date, @Status, @Processed_by, @Total_amount)
            RETURNING id",
                    new
                    {
                        order.Customer_name,
                        order.Customer_contact,
                        order.Order_date,
                        order.Status,
                        Processed_by = order.Processed_by ?? (object)DBNull.Value,
                        Total_amount = order.Total_amount ?? 0
                    }
                );
            }
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                var affected = await connection.ExecuteAsync(@"
                    UPDATE orders
                    SET customer_name = @Customer_Name,
                        customer_contact = @Customer_Contact,
                        order_date = @Order_Date,
                        status = @Status,
                        processed_by = @Processed_By,
                        total_amount = @Total_Amount
                    WHERE id = @Id",
                    order
                );
                return affected > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                var affected = await connection.ExecuteAsync(
                    "DELETE FROM orders WHERE id = @Id",
                    new { Id = id }
                );
                return affected > 0;
            }
        }

        public async Task<IEnumerable<Order>> GetByCustomerAsync(int customer_Id)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Order>(
                    "SELECT * FROM orders WHERE customer_id = @Customer_Id",
                    new { Customer_Id = customer_Id }
                );
            }
        }

        public async Task<IEnumerable<Order>> GetByStatusAsync(string status)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Order>(
                    "SELECT * FROM orders WHERE status = @Status",
                    new { Status = status }
                );
            }
        }

        public async Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                var query = @"
        SELECT 
            o.id AS Id,
            o.customer_name AS CustomerName,
            o.order_date AS OrderDate,
            o.status AS Status,
            o.total_amount AS TotalAmount,
            oi.order_id AS OrderId,
            oi.product_id AS ProductId,
            oi.quantity AS Quantity,
            oi.unit_price AS UnitPrice,
            p.id AS ProductId,
            p.name AS ProductName
        FROM orders o
        LEFT JOIN order_items oi ON o.id = oi.order_id
        LEFT JOIN products p ON oi.product_id = p.id
        WHERE o.order_date BETWEEN @StartDate AND @EndDate";

                var orderDict = new Dictionary<int, Order>();

                var orders = await connection.QueryAsync<Order, OrderItem, Product, Order>(
                    query,
                    (order, orderItem, product) =>
                    {
                        if (!orderDict.TryGetValue(order.Id, out var existingOrder))
                        {
                            existingOrder = order;
                            existingOrder.Items = new List<OrderItem>();
                            orderDict.Add(existingOrder.Id, existingOrder);
                        }

                        if (orderItem != null)
                        {
                            orderItem.Product = product ?? new Product { Name = "Неизвестно" };
                            existingOrder.Items.Add(orderItem);
                        }

                        return existingOrder;
    },
                    new { StartDate = startDate, EndDate = endDate },
        splitOn: "OrderId,ProductId"  // Теперь столбцы существуют в результате запроса
                );

                return orderDict.Values.ToList();
            }
        }
    }
}