using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.BusinessLogic
{
    public class OrderDetailsService
    {
        private readonly IOrderDetailsRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork; // Đổi sang IUnitOfWork

        // Sử dụng Dependency Injection để nhận các repository và Unit of Work
        /*public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository, IProductRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = orderDetailsRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }*/

        public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = orderDetailsRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // --- TRA CỨU ---
        // Lấy danh sách chi tiết đơn hàng theo mã đơn hàng
        public List<OrderDetailsDTO> GetByOrderID(int orderId)
        {
            var entities = _repository.GetByOrderID(orderId);
            var dtos = _mapper.Map<List<OrderDetailsDTO>>(entities);
            return dtos;
        }

        // Lấy chi tiết đơn hàng theo ID
        public OrderDetailsDTO? GetById(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                return null;
            }
            var dto = _mapper.Map<OrderDetailsDTO>(entity);
            return dto;
        }

        // --- THÊM MỚI ---
        public void Add(OrderDetailsDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto), "Order detail data cannot be null.");
            var entity = _mapper.Map<Order_Details>(dto);
            _repository.Insert(entity);
            _unitOfWork.SaveChanges(); // LƯU THAY ĐỔI thông qua UnitOfWork
        }

        public void AddOrderDetails(List<OrderDetailsDTO> dtos)
        {
            if (dtos == null || !dtos.Any()) return;
            var entities = _mapper.Map<List<Order_Details>>(dtos);
            _repository.AddRange(entities);
            _unitOfWork.SaveChanges(); // LƯU THAY ĐỔI thông qua UnitOfWork
        }

        // --- CẬP NHẬT ---
        public void Update(int id, OrderDetailsDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto), "Order detail data cannot be null.");
            var existingEntity = _repository.GetById(id);
            if (existingEntity == null)
            {
                throw new Exception($"Order detail with ID {id} not found.");
            }
            _mapper.Map(dto, existingEntity);
            _repository.Update(existingEntity);
            _unitOfWork.SaveChanges(); // LƯU THAY ĐỔI thông qua UnitOfWork
        }

        // Phương thức này có thể không cần thiết nếu bạn đã có logic trong OrderService
        public void UpdateOrderDetails(int orderId, List<OrderDetailsDTO> dtos)
        {
            _repository.DeleteByOrderID(orderId);
            if (dtos != null && dtos.Any())
            {
                var entities = _mapper.Map<List<Order_Details>>(dtos);
                _repository.AddRange(entities);
            }
            _unitOfWork.SaveChanges(); // LƯU THAY ĐỔI thông qua UnitOfWork
        }

        // --- XÓA ---
        public void Delete(int id)
        {
            var entityToDelete = _repository.GetById(id);
            if (entityToDelete == null)
            {
                throw new Exception($"Order detail with ID {id} not found for deletion.");
            }
            _repository.Delete(entityToDelete);
            _unitOfWork.SaveChanges(); // LƯU THAY ĐỔI thông qua UnitOfWork
        }

        public void DeleteByOrderId(int orderId)
        {
            _repository.DeleteByOrderID(orderId);
            _unitOfWork.SaveChanges(); // LƯU THAY ĐỔI thông qua UnitOfWork
        }
    }
}