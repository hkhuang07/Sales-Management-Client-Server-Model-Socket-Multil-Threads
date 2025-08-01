// File: ProductService.cs
using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;
using SlugGenerator;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ElectronicsStore.BusinessLogic
{
    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public ProductService(IProductRepository repository, IMapper mapper, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

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

        //Thêm mới (Đã sửa: Trả về đối tượng đã thêm)
        public ProductDTO Add(ProductDTO dto)
        {
            Validate(dto);
            var entity = _mapper.Map<Products>(dto);
            _repository.Add(entity);
            _unitOfWork.SaveChanges();

            // Sau khi lưu, entity sẽ có ID mới. Map lại và trả về cho client.
            return _mapper.Map<ProductDTO>(entity);
        }

        //Cập nhật (Đã sửa: Trả về đối tượng đã cập nhật)
        public ProductDTO Update(int id, ProductDTO dto)
        {
            Validate(dto);

            var existing = _repository.GetById(id);
            if (existing == null)
                throw new Exception($"Product not found with ID = {id}.");

            // Cập nhật các thuộc tính
            existing.ProductName = dto.ProductName;
            existing.Price = dto.Price;
            existing.Quantity = dto.Quantity;
            existing.Description = dto.Description;
            existing.ManufacturerID = dto.ManufacturerID;
            existing.CategoryID = dto.CategoryID;

            // Nếu bạn muốn cập nhật luôn cả tên ảnh
            existing.Image = dto.Image;

            _repository.Update(existing);
            _unitOfWork.SaveChanges();

            return _mapper.Map<ProductDTO>(existing);
        }
        public byte[]? GetProductImage(string fileName)
        {
            // Xác định thư mục lưu trữ ảnh. 
            // Dòng code này giả định thư mục "Images/Products" nằm trong thư mục gốc của server.
            string serverAppPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string imageDirectory = Path.Combine(serverAppPath, "Images", "Products");
            string filePath = Path.Combine(imageDirectory, fileName);

            // Xử lý tên file rỗng hoặc không hợp lệ
            if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(filePath))
            {
                // Trả về null nếu không tìm thấy file
                return null;
            }

            try
            {
                // Đọc toàn bộ file thành mảng byte và trả về
                return File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading image file {filePath}: {ex.Message}");
                return null;
            }
        }

        // Cập nhật hình ảnh (Phương thức này vẫn giữ nguyên vì nó chỉ cập nhật một trường)
        public void UpdateImage(int productId, string imageFileName)
        {
            using(var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(imageFileName) || imageFileName.Length > 250)
                        throw new ArgumentException("Image path must be under 250 characters.");

                    var existingProduct = _repository.GetById(productId);
                    if (existingProduct == null)
                        throw new Exception($"Product not found with ID = {productId}.");

                    _repository.UpdateImage(productId, imageFileName);
                    _unitOfWork.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                } 
                
            }    
        }

        //Delete (Đã sửa: Trả về bool để báo cáo kết quả)
        public bool Delete(int id)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var entity = _repository.GetById(id);
                    if (entity == null)
                        return false; // Trả về false nếu không tìm thấy

                    _repository.Delete(entity);
                    _unitOfWork.SaveChanges();
                    transaction.Commit();

                    return true; // Trả về true nếu xóa thành công
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }    

          }
    }
}