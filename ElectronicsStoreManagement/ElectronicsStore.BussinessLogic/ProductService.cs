using System;
using System.Collections.Generic;
using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;
using Microsoft.EntityFrameworkCore;
using SlugGenerator;

namespace ElectronicsStore.BusinessLogic
{
    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper)
        {
            _repository = new ProductRepository();
            _mapper = mapper;
        }

        //Kiểm tra dữ liệu đầu vào
        private void Validate(ProductDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Product data cannot be null.");

            if (string.IsNullOrWhiteSpace(dto.ProductName) || dto.ProductName.Length > 200)
                throw new ArgumentException("Product name cannot be blank and must be under 200 characters.");

            if (dto.Price < 0)
                throw new ArgumentException("Price must be a non-negative number.");

            if (dto.Quantity < 0)
                throw new ArgumentException("Quantity must be a non-negative number.");

            if (!string.IsNullOrWhiteSpace(dto.Image) && dto.Image.Length > 250)
                throw new ArgumentException("Image path must be under 250 characters.");

            if (!string.IsNullOrWhiteSpace(dto.Description) && dto.Description.Length > 500)
                throw new ArgumentException("Description must be under 500 characters.");

            if (dto.ManufacturerID <= 0)
                throw new ArgumentException("Manufacturer must be selected.");

            if (dto.CategoryID <= 0)
                throw new ArgumentException("Category must be selected.");
        }

        //Tra cứu
        public List<ProductList> GetAllList()
        {
            var products = _repository.GetAllWithCategoryManufacturer();
            return _mapper.Map<List<ProductList>>(products);
        }
     
        public List<ProductDTO> GetAll()
        {
            var list = _repository.GetAll();
            return _mapper.Map<List<ProductDTO>>(list);
        }

        public ProductDTO? GetById(int id)
        {
            var product = _repository.GetById(id);
            return product != null ? _mapper.Map<ProductDTO>(product) : null;
        }

        public ProductDTO? GetByName(string key)
        {
            var product = _repository.GetByName(key);
            return product != null ? _mapper.Map<ProductDTO>(product) : null;
        }

        //Thêm mới
        public void Add(ProductDTO dto)
        {
            Validate(dto);
            var entity = _mapper.Map<Products>(dto);
            _repository.Add(entity);
        }


        //Cập nhật
        /*public void Update(int id, ProductDTO dto)
        {
            Validate(dto);

            var existing = _repository.GetById(id);
            if (existing == null)
                throw new Exception($"Product not found with ID = {id}.");

            // Map các thuộc tính từ dto sang entity cũ
            _mapper.Map(dto, existing);
            _repository.Update(existing);
        }*/

        public void Update(int id, ProductDTO dto)
        {
            Validate(dto);

            var existing = _repository.GetById(id);
            if (existing == null)
                throw new Exception($"Product not found with ID = {id}.");

            // Chỉ cập nhật các thuộc tính cần thiết
            existing.ProductName = dto.ProductName;
            existing.Price = dto.Price;
            existing.Quantity = dto.Quantity;
            //existing.Image = dto.Image;
            existing.Description = dto.Description;
            existing.ManufacturerID = dto.ManufacturerID;
            existing.CategoryID = dto.CategoryID;

            _repository.Update(existing);
        }

        public void UpdateImage(int productId, string imageFileName)
        {
            if (string.IsNullOrWhiteSpace(imageFileName) || imageFileName.Length > 250)
                throw new ArgumentException("Image path must be under 250 characters.");

            var existingProduct = _repository.GetById(productId);
            if (existingProduct == null)
                throw new Exception($"Product not found with ID = {productId}.");

            // Chỉ cập nhật thuộc tính Image
            existingProduct.Image = imageFileName;

            // Lưu thay đổi
            _repository.UpdateImage(productId, imageFileName);
        }

        //Delete
        public void Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception($"Product not found with ID = {id}.");

            _repository.Delete(entity);
        }

    }
}
