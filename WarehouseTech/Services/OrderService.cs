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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }
        public async Task CreateOrderWithItemsAsync(Order order, IEnumerable<OrderItem> items)
        {
            order.Total_amount = items.Sum(i => i.Quantity * i.Unit_price);
            int orderId = await _orderRepository.AddAsync(order);

            foreach (var item in items)
            {
                item.Order_id = orderId;
                await _orderItemRepository.AddAsync(item);
            }
        }
        public async Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.GetByDateRangeAsync(startDate, endDate);
        }
        public async Task<int> AddOrderAsync(Order order)
        {
            // Логика добавления нового заказа
            return await _orderRepository.AddAsync(order);
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var isUpdated = await _orderRepository.UpdateAsync(order);
            if (isUpdated)
            {
                // Удаляем старые элементы
                await _orderItemRepository.DeleteByOrderAsync(order.Id);

                // Добавляем новые
                foreach (var item in order.Items)
                {
                    item.Order_id = order.Id;
                    await _orderItemRepository.AddAsync(item);
                }
            }
            return isUpdated;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            // Логика получения всех заказов
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            // Логика получения заказа по id
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            // Логика получения заказов по клиенту
            return await _orderRepository.GetByCustomerAsync(customerId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            // Логика получения заказов по статусу
            return await _orderRepository.GetByStatusAsync(status);
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            // Логика получения заказов по диапазону дат
            return await _orderRepository.GetByDateRangeAsync(startDate, endDate);
        }
    }

}
