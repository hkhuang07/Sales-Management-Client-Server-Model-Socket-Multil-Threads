using ElectronicsStore.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataAccess
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ElectronicsStoreContext _context;

        public CategoryRepository()
        {
            _context = new ElectronicsStoreContext();
        }

        //Tra cứu
        public List<Categories> GetAll() => _context.Category.ToList();

        public Categories? GetById(int id) => _context.Category.Find(id);

        /*public CategoryDTO GetById(int id)
        {
            return _context.Category.Find(x => x.Id == id);
        }*/

        //Thêm mới
        public void Add(Categories category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();
        }

        //Cập nhật
        public void Update(Categories category)
        {
            var existingCategory = _context.Category.Find(category.ID);
            if (existingCategory != null)
            {
                // Cập nhật các thuộc tính cần thiết
                existingCategory.CategoryName = category.CategoryName;
                _context.Category.Update(existingCategory);
                _context.SaveChanges();
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
            _context.SaveChanges();
        }

    }
}
