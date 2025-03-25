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
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public ShipmentRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<IEnumerable<Shipment>> GetAllAsync()
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var query = "SELECT * FROM shipments";
                return await connection.QueryAsync<Shipment>(query);
            }
        }

        public async Task<Shipment> GetByIdAsync(int id)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                // Используем имя столбца "id"
                var query = "SELECT * FROM shipments WHERE id = @Id";
                return await connection.QuerySingleOrDefaultAsync<Shipment>(query, new { Id = id });
            }
        }

        public async Task<int> AddAsync(Shipment shipment)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                // Вставляем только те столбцы, которые есть: supplier_id, shipment_date, received_by и total_cost
                var query = @"
                INSERT INTO shipments (supplier_id, shipment_date, received_by, total_cost)
                VALUES (@supplier_id, @shipment_date, @received_by, @total_cost)
                RETURNING id";
                return await connection.QuerySingleAsync<int>(query, shipment);
            }
        }

        public async Task<bool> UpdateAsync(Shipment shipment)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var query = @"
                UPDATE shipments
                SET supplier_id = @supplier_id, 
                    shipment_date = @shipment_date, 
                    received_by = @received_by, 
                    total_cost = @total_cost
                WHERE id = @Id";
                var result = await connection.ExecuteAsync(query, shipment);
                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var query = "DELETE FROM shipments WHERE id = @Id";
                var result = await connection.ExecuteAsync(query, new { Id = id });
                return result > 0;
            }
        }

        public async Task<IEnumerable<Shipment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var query = @"
            SELECT 
                s.id AS Id,
                s.supplier_id AS Supplier_id,
                s.shipment_date AS Shipment_date,
                s.received_by AS Received_by,
                s.total_cost AS Total_cost,
                sup.id AS Supplier_Id,
                sup.name AS Name,
                sup.contact_phone AS Contact_phone,
                sup.contact_email AS Contact_email,
                sup.address AS Address,
                sup.created_at AS Created_at,
                si.shipment_id AS Shipment_id,
                si.product_id AS Product_id,
                si.quantity AS Quantity,
                si.unit_price AS Unit_price,
                p.id AS Product_id,
                p.name AS Name,
                p.description AS Description,
                p.price AS Price,
                p.quantity AS Product_Quantity,
                p.created_at AS Product_Created_At,
                p.updated_at AS Product_Updated_At
            FROM shipments s
            INNER JOIN suppliers sup ON s.supplier_id = sup.id
            LEFT JOIN shipment_items si ON s.id = si.shipment_id
            LEFT JOIN products p ON si.product_id = p.id
            WHERE s.shipment_date BETWEEN @StartDate AND @EndDate";

                var shipmentDict = new Dictionary<int, Shipment>();

                var shipments = await connection.QueryAsync<Shipment, Supplier, ShipmentItem, Product, Shipment>(
                    query,
                    (shipment, supplier, shipmentItem, product) =>
                    {
                        if (!shipmentDict.TryGetValue(shipment.Id, out var existingShipment))
                        {
                            existingShipment = shipment;
                            existingShipment.Supplier = supplier;
                            existingShipment.Items = new List<ShipmentItem>();
                            shipmentDict.Add(existingShipment.Id, existingShipment);
                        }

                        if (shipmentItem != null)
                        {
                            shipmentItem.Product = product ?? new Product { Name = "Неизвестно" };
                            existingShipment.Items.Add(shipmentItem);
                        }

                        return existingShipment;
                    },
                    new { StartDate = startDate, EndDate = endDate },
                    splitOn: "Supplier_Id,Shipment_id,Product_Id"
                );

                return shipmentDict.Values.ToList();
            }
        }

        public async Task<IEnumerable<Shipment>> GetBySupplierAsync(int supplierId)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                var query = "SELECT * FROM shipments WHERE supplier_id = @supplier_id";
                return await connection.QueryAsync<Shipment>(query, new { supplier_id = supplierId });
            }
        }
    }



}
