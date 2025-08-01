using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;

namespace ElectronicsStore.BusinessLogic
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork; // Thêm UnitOfWork


        // Constructor đã được cập nhật để nhận ICategoryRepository qua DI
        public CategoryService(CategoryRepository repository, IMapper mapper, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //Tra cứu 
        public List<CategoryDTO> GetAll()
        {
            var list = _repository.GetAll();
            return _mapper.Map<List<CategoryDTO>>(list);
        }

        public CategoryDTO GetById(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception("Category not found!");
            return _mapper.Map<CategoryDTO>(entity);
        }

        public List<CategoryDTO> GetByName(string name)
        {
            var list = _repository.GetAll().Where(c => c.CategoryName.Contains(name)).ToList();
            return _mapper.Map<List<CategoryDTO>>(list);
        }

        //Thêm mới
        public void Add(CategoryDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CategoryName))
                throw new ArgumentException("Category Name cannot be left blank!");

            var entity = _mapper.Map<Categories>(dto);
            _repository.Add(entity);
            _unitOfWork.SaveChanges();
        }

        //Cập nhật
        public void Update(int id, CategoryDTO dto)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new Exception("Category not found!");

            entity.CategoryName = dto.CategoryName;
            _repository.Update(entity);
            _unitOfWork.SaveChanges();

        }

        //Xóa
        public void Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new Exception("Category not found!");
            _repository.Delete(entity);
            _unitOfWork.SaveChanges();
        }
    }
}