using ElectronicsStore.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicsStore.DataAccess
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ElectronicsStoreContext _context;

        // Constructor đã được cập nhật để nhận DbContext qua Dependency Injection
        public CategoryRepository(ElectronicsStoreContext context)
        {
            _context = context;
        }

        //Tra cứu
        public List<Categories> GetAll() => _context.Category.ToList();

        public Categories? GetById(int id) => _context.Category.Find(id);

        //Thêm mới
        public void Add(Categories category)
        {
            _context.Category.Add(category);
            // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
        }

        //Cập nhật
        public void Update(Categories category)
        {
            var existingCategory = _context.Category.Find(category.ID);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                _context.Category.Update(existingCategory);
                // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
            }
            else
            {
                throw new Exception($"Category with ID = {category.ID} not found.");
            }
        }

        //Xóa
        public void Delete(Categories category)
        {
            _context.Category.Remove(category);
            // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
        }
    }
}