using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;
using WarehouseTech.Repositories;
using WarehouseTech.Repositories.Interfaces;
using WarehouseTech.Services.Interfaces;

namespace WarehouseTech.Services
{
    public class ReportService : IReportService
    {
        private readonly IProductRepository _productRepository;
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IShipmentItemRepository _shipmentItemRepository;

        public ReportService(IOrderItemRepository orderItemRepository, IProductRepository productRepository, IShipmentRepository shipmentRepository, IOrderRepository orderRepository, IShipmentItemRepository shipmentItemRepository)
        {
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _shipmentRepository = shipmentRepository;
            _orderRepository = orderRepository;
            _shipmentItemRepository = shipmentItemRepository;
        }
        public async Task<IEnumerable<PopularProductReport>> GetPopularProductsReportAsync()
        {
            var orderItems = await _orderItemRepository.GetAllAsync();
            var products = await _productRepository.GetAllAsync();

            var groupedItems = orderItems
                .GroupBy(oi => oi.Product_id)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSoldQuantity = g.Sum(oi => oi.Quantity),
                    TotalOrders = g.Select(oi => oi.Order_id).Distinct().Count()
                });

            return groupedItems
                .Join(products,
                    g => g.ProductId,
                    p => p.Id,
                    (g, p) => new PopularProductReport
                    {
                        ProductName = p.Name, // Исправлено с Product_Name на ProductName
                        TotalSoldQuantity = g.TotalSoldQuantity,
                        TotalOrders = g.TotalOrders,
                        PopularityRank = 0
                    })
                .OrderByDescending(x => x.TotalSoldQuantity)
                .Select((x, index) =>
                {
                    x.PopularityRank = index + 1;
                    return x;
                })
                .ToList();
        }
        public async Task<IEnumerable<InventoryReport>> GetInventoryReportAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var inventoryReports = new List<InventoryReport>();

            foreach (var product in products)
            {
                var supplied = (await _shipmentItemRepository.GetByProductAsync(product.Id)).Sum(si => si.Quantity);
                var sold = (await _orderItemRepository.GetByProductAsync(product.Id)).Sum(oi => oi.Quantity);

                inventoryReports.Add(new InventoryReport
                {
                    ProductName = product.Name,
                    CurrentPrice = product.Price,
                    CurrentQuantity = product.Quantity,
                    TotalSupplied = supplied,
                    TotalSold = sold
                });
            }

            return inventoryReports;
        }

        public async Task<IEnumerable<ShipmentStatistics>> GetShipmentStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            var shipments = await _shipmentRepository.GetByDateRangeAsync(startDate, endDate);
            return shipments.Select(s => new ShipmentStatistics
            {
                ShipmentDate = s.Shipment_date,
                SupplierName = s.Supplier?.Name ?? "Неизвестно",
                TotalQuantity = s.Items?.Sum(i => i.Quantity) ?? 0,
                TotalCost = s.Total_cost ?? 0
            });
        }

        public async Task<IEnumerable<OrderStatistics>> GetOrderStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderRepository.GetByDateRangeAsync(startDate, endDate);
            return orders.Select(o => new OrderStatistics
            {
                OrderDate = o.Order_date,
                CustomerName = o.Customer_name,
                TotalProductsTypes = o.Items?.Select(i => i.Product_id).Distinct().Count() ?? 0,
                TotalQuantity = o.Items?.Sum(i => i.Quantity) ?? 0,
                TotalAmount = o.Items?.Sum(i => i.Quantity * i.Unit_price) ?? 0
            });
        }

        public async Task<IEnumerable<ProductMovementReport>> GetProductMovementReportAsync()
        {
            // Логика для получения отчета по движению товаров
            var shipments = await _shipmentRepository.GetAllAsync();
            var orders = await _orderRepository.GetAllAsync();
            var productMovementReports = new List<ProductMovementReport>();

            foreach (var shipment in shipments)
            {
                foreach (var item in shipment.Items)
                {
                    var report = new ProductMovementReport
                    {
                        ProductId = item.Product_id, // Свойство переименовано
                        QuantityIn = item.Quantity, // Свойство переименовано
                        MovementDate = shipment.Shipment_date // Свойство переименовано
                    };
                    productMovementReports.Add(report);
                }
            }

            foreach (var order in orders)
            {
                foreach (var item in order.Items)
                {
                    var report = new ProductMovementReport
                    {
                        ProductId = item.Product_id,
                        QuantityOut = item.Quantity,
                        MovementDate = order.Order_date
                    };
                    productMovementReports.Add(report);
                }
            }

            return productMovementReports;
        }
    }

}
