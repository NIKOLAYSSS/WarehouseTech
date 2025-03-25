using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTech.Models;
using WarehouseTech.Repositories.Interfaces;
using WarehouseTech.Services.Interfaces;

namespace WarehouseTech.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            // Логика добавления нового товара
            return await _productRepository.AddAsync(product);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            // Логика обновления информации о товаре
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            // Логика получения всех товаров
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            // Логика получения товара по id
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            // Логика получения товаров по категории
            var products = await _productRepository.GetByCategoryAsync(categoryId.ToString());

            // Используем async в лямбде, если нужно
            return products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            });
        }

        public async Task<int> GetProductStockAsync(int productId)
        {
            // Логика получения остатка на складе для товара
            var products = await _productRepository.GetStockAsync(productId);
            return products.Sum(p => p.Quantity); // Предполагается, что в модели Product есть свойство Quantity
        }
        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteAsync(productId);
        }
    }

}
