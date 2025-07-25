using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private readonly ElectronicsStoreContext _context;
        public ProductRepository()
        {
            _context = new ElectronicsStoreContext();
        }
    
        public List<Products> GetAll() => _context.Product.ToList();

        public Products? GetById(int id) => _context.Product.Find(id);

        public Products? GetByName(string key) => _context.Product.Find(key);
        public void Add(Products product)
        {
            if (_context.Product.Any(p => p.ID == product.ID))
                throw new Exception($"Product with ID = {product.ID} already exists.");

            _context.Product.Add(product);
            _context.SaveChanges();
        }

        /*public void Add(Products product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
        }

        public void Update(Products product)
        {
            _context.Product.Update(product);
            _context.SaveChanges();
        }*/

        public void Update(Products product)
        {
            var existingProduct = _context.Product.Find(product.ID);
            if (existingProduct != null)
            {
                // Cập nhật các thuộc tính cần thiết
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;
                //existingProduct.Image = product.Image;
                existingProduct.Description = product.Description;
                existingProduct.ManufacturerID = product.ManufacturerID;
                existingProduct.CategoryID = product.CategoryID;
                _context.Product.Update(existingProduct);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Product with ID = {product.ID} not found.");
            }
        }

        //Cập nhật hình ảnh
        public void UpdateImage(int productId, string imageFileName)
        {
            if (string.IsNullOrWhiteSpace(imageFileName) || imageFileName.Length > 250)
                throw new ArgumentException("Image path must be under 250 characters.");
            var existingProduct = _context.Product.Find(productId);
            if (existingProduct != null)
            {
                existingProduct.Image = imageFileName;
                _context.Product.Update(existingProduct);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Product with ID = {productId} not found.");
            }
        }

        public void Delete(Products product)
        {
            _context.Product.Remove(product);
            _context.SaveChanges();
        }

        public List<Products> GetAllWithCategoryManufacturer()
        {
            using var context = new ElectronicsStoreContext(); // Replace AppDbContext with ElectronicsStoreContext
            return context.Product
                         .Include(p => p.Category)
                         .Include(p => p.Manufacturer)
                         .ToList();
        }

    }
}
