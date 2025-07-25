// Trong OrderDetailsService.cs

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;

namespace ElectronicsStore.BusinessLogic
{
    public class OrderDetailsService
    {
        private readonly IOrderDetailsRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderDetailsService(IMapper mapper)
        {
            _repository = new OrderDetailsRepository();
            _productRepository = new ProductRepository();
            _mapper = mapper;
        }

        // --- TRA CỨU ---
        // Lấy danh sách chi tiết đơn hàng theo mã đơn hàng
        public List<OrderDetailsDTO> GetByOrderID(int orderId)
        {
            var entities = _repository.GetByOrderID(orderId);
            var dtos = _mapper.Map<List<OrderDetailsDTO>>(entities);

            // Gán thêm tên sản phẩm
            foreach (var dto in dtos)
            {
                // Đảm bảo _productRepository không null và GetById trả về giá trị
                dto.ProductName = _productRepository?.GetById(dto.ProductID)?.ProductName;
            }

            return dtos;
        }

        // THÊM MỚI: Lấy chi tiết đơn hàng theo ID
        public OrderDetailsDTO? GetById(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                return null;
            }
            var dto = _mapper.Map<OrderDetailsDTO>(entity);
            dto.ProductName = _productRepository?.GetById(dto.ProductID)?.ProductName;
            return dto;
        }

        // --- THÊM MỚI ---
        // Phương thức AddOrderDetail (số ít) nếu bạn muốn thêm từng chi tiết
        public void Add(OrderDetailsDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto), "Order detail data cannot be null.");
            var entity = _mapper.Map<Order_Details>(dto);
            _repository.Insert(entity); // Hoặc một phương thức Add(entity) trong Repository nếu bạn có
        }

        public void AddOrderDetails(List<OrderDetailsDTO> dtos)
        {
            if (dtos == null || !dtos.Any()) return;
            var entities = _mapper.Map<List<Order_Details>>(dtos);
            _repository.AddRange(entities);
        }

        // --- CẬP NHẬT ---
        // Phương thức UpdateOrderDetail (số ít) nếu bạn muốn cập nhật từng chi tiết
        public void Update(int id, OrderDetailsDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto), "Order detail data cannot be null.");
            var existingEntity = _repository.GetById(id);
            if (existingEntity == null)
            {
                throw new Exception($"Order detail with ID {id} not found.");
            }
            // Ánh xạ các thuộc tính từ DTO vào entity hiện có
            _mapper.Map(dto, existingEntity);
            _repository.Update(existingEntity);
        }

        public void UpdateOrderDetails(int orderId, List<OrderDetailsDTO> dtos)
        {
            _repository.DeleteByOrderID(orderId);
            if (dtos != null && dtos.Any())
            {
                var entities = _mapper.Map<List<Order_Details>>(dtos);
                _repository.AddRange(entities);
            }
        }

        // --- XÓA ---
        // THÊM MỚI: Xóa một chi tiết đơn hàng riêng lẻ
        public void Delete(int id)
        {
            var entityToDelete = _repository.GetById(id);
            if (entityToDelete == null)
            {
                throw new Exception($"Order detail with ID {id} not found for deletion.");
            }
            _repository.Delete(entityToDelete);
        }

        // Phương thức này đã có sẵn trong OrderDetailsRepository, nhưng bạn có thể tạo một wrapper ở đây
        // public void DeleteByOrderId(int orderId)
        // {
        //     _repository.DeleteByOrderID(orderId);
        // }


    }
}