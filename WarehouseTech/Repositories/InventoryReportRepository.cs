using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;
using WarehouseTech.Repositories.Interfaces;
using Dapper;

namespace WarehouseTech.Repositories
{
    public class InventoryReportRepository : GenericRepository<InventoryReport>, IReportRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public InventoryReportRepository(DatabaseConnection databaseConnection) : base(databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<IEnumerable<InventoryReport>> GetInventoryReportAsync()
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                // Запрос на получение данных для отчета
                var query = "SELECT * FROM inventory_report"; // Предположим, что это представление
                return await connection.QueryAsync<InventoryReport>(query);
            }
        }

        public async Task<IEnumerable<ShipmentStatistics>> GetShipmentStatisticsAsync()
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                // Запрос на получение данных для статистики поставок
                var query = "SELECT * FROM shipments_statistics"; // Представление для статистики поставок
                return await connection.QueryAsync<ShipmentStatistics>(query);
            }
        }

        public async Task<IEnumerable<OrderStatistics>> GetOrderStatisticsAsync()
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                // Запрос на получение данных для статистики заказов
                var query = "SELECT * FROM orders_statistics"; // Представление для статистики заказов
                return await connection.QueryAsync<OrderStatistics>(query);
            }
        }

        public async Task<IEnumerable<ProductMovementReport>> GetProductMovementReportAsync()
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                // Запрос на получение данных для отчета движения товаров
                var query = "SELECT * FROM product_movement_report"; // Представление для отчета движения товаров
                return await connection.QueryAsync<ProductMovementReport>(query);
            }
        }
    }

}
