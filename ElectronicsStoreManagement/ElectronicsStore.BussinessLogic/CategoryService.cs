using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;

namespace ElectronicsStore.BusinessLogic
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper)
        {
            _repository = new CategoryRepository();
            _mapper = mapper;
        }

        //Tra cứu 
        public List<CategoryDTO> GetAll()
        {
            var list = _repository.GetAll();
            return _mapper.Map<List<CategoryDTO>>(list);
        }
        /*public List<CategoryDTO> GetCategories()
        {
            var list = _repository.GetAll();
            return _mapper.Map<List<CategoryDTO>>(list);
        } */
        public CategoryDTO GetById(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new Exception("Category not found!");
            return _mapper.Map<CategoryDTO>(entity);
        }
        public List<CategoryDTO> GetByName(string name)
        {
            var list = _repository.GetAll().Where(c => c.CategoryName.Contains(name)).ToList();
            return _mapper.Map<List<CategoryDTO>>(list);
        }
        /*public CategoryDTO GetById(int id) 
        { Logic lấy danh mục theo ID 
            return null; 
        }*/

        //Thêm mới
        public void Add(CategoryDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CategoryName))
                throw new ArgumentException("Category Name cannot be left blank!");

            var entity = _mapper.Map<Categories>(dto);
            _repository.Add(entity);
        }

        //Cập nhật
        public void Update(int id, CategoryDTO dto)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new Exception("Category not found!");

            entity.CategoryName = dto.CategoryName;
            _repository.Update(entity);
        }

        //Xóa
        public void Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new Exception("Category not found!");
            _repository.Delete(entity);
        }

    }

}