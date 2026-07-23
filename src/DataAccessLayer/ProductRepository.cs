using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private readonly ElectronicsStoreContext _context;

        // Constructor đã được cập nhật để nhận DbContext qua Dependency Injection
        public ProductRepository(ElectronicsStoreContext context)
        {
            _context = context;
        }
        public List<Products> GetAllWithCategoryManufacturer()
        {
            return _context.Product
                           .Include(p => p.Category)
                           .Include(p => p.Manufacturer)
                           .ToList();
        }

        public List<Products> GetAll()
        {
            return _context.Product
                            .Include(p => p.Category)
                            .Include(p => p.Manufacturer)
                            .ToList();
        }
        public Products? GetById(int id) => _context.Product.Find(id);

        public Products? Get1ByName(string key) => _context.Product.FirstOrDefault(p => p.ProductName == key); // Đã sửa lỗi: Find() không hoạt động với chuỗi

        // ProductRepository.cs
        public List<Products> GetByName(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                // Gọi phương thức GetAll() đã được cập nhật
                return GetAll();
            }

            // Sử dụng .Include() để tải kèm theo dữ liệu của Category và Manufacturer
            return _context.Product
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Where(p => EF.Functions.Like(p.ProductName, $"%{key}%") || EF.Functions.Like(p.Description, $"%{key}%"))
                .ToList();
        }
        public void Add(Products product)
        {
            if (_context.Product.Any(p => p.ID == product.ID))
                throw new Exception($"Product with ID = {product.ID} already exists.");

            _context.Product.Add(product);
            // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
        }

        public void Update(Products product)
        {
            var existingProduct = _context.Product.Find(product.ID);
            if (existingProduct != null)
            {
                // Cập nhật các thuộc tính cần thiết
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;
                existingProduct.Description = product.Description;
                existingProduct.ManufacturerID = product.ManufacturerID;
                existingProduct.CategoryID = product.CategoryID;
                _context.Product.Update(existingProduct);
                // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
            }
            else
            {
                throw new Exception($"Product with ID = {product.ID} not found.");
            }
        }

        public void UpdateImage(int productId, string imageFileName)
        {
            if (string.IsNullOrWhiteSpace(imageFileName) || imageFileName.Length > 250)
                throw new ArgumentException("Image path must be under 250 characters.");
            var existingProduct = _context.Product.Find(productId);
            if (existingProduct != null)
            {
                existingProduct.Image = imageFileName;
                _context.Product.Update(existingProduct);
                // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
            }
            else
            {
                throw new Exception($"Product with ID = {productId} not found.");
            }
        }

        public void Delete(Products product)
        {
            _context.Product.Remove(product);
            // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
        }

        
    }
}