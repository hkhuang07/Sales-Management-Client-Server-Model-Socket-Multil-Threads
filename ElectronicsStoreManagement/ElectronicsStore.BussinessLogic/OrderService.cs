﻿using System;
using System.Collections.Generic;
using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.BusinessLogic
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderDetailsRepository _orderdetailsrepository;
        private readonly IMapper _mapper;

        public OrderService(IMapper mapper)
        {
            _repository = new OrderRepository();
            _orderdetailsrepository = new OrderDetailsRepository();
            _mapper = mapper;
        }

        //Tra cứu
        public List<OrderList> GetAllList()
        {
            var orders = _repository.GetAllWithDetails();
            return _mapper.Map<List<OrderList>>(orders);
        }

        public OrderDTO? GetById(int id)
        {
            var order = _repository.GetById(id);
            return order != null ? _mapper.Map<OrderDTO>(order) : null;
        }

        //Thêm mới
        //thêm mới đơn hàng không có chi tiết đơn hàng
        public void Add(OrderDTO dto)   
        {
            if (dto == null) throw new ArgumentException("Order data cannot be null.");
            var entity = _mapper.Map<Orders>(dto);
            _repository.Add(entity);
        }

        //thêm mới đơn hàng và chi tiết đơn hàng
        public int CreateOrder(OrderDTO orderDto, List<OrderDetailsDTO> details)
        {
            var order = _mapper.Map<Orders>(orderDto);
            order.Date = DateTime.Now;

            int orderId = _repository.Insert(order);

            foreach (var detailDto in details)
            {
                var detail = _mapper.Map<Order_Details>(detailDto);
                detail.OrderID = orderId;
                _orderdetailsrepository.Insert(detail);
            }

            return orderId;
        }

        //Cập nhật
        //Cập nhật đơn hàng không có chi tiết đơn hàng
        public void Update(int id, OrderDTO dto)
        {
            if (dto == null) throw new ArgumentException("Order data cannot be null.");

            var existing = _repository.GetById(id);
            if (existing == null)
                throw new Exception($"Order not found with ID = {id}.");

            // Cập nhật thông tin cơ bản
            existing.Date = dto.Date;                                                               
            existing.EmployeeID = dto.EmployeeID;
            existing.CustomerID = dto.CustomerID;
            existing.Note = dto.Note;

            _repository.Update(existing);
        }
        //Cập nhật đơn hàng và chi tiết đơn hàng
        public void UpdateOrder(OrderDTO orderDto, List<OrderDetailsDTO> details)
        {
            // Xóa chi tiết cũ trước
            _orderdetailsrepository.DeleteByOrderID(orderDto.ID);

            // Cập nhật đơn hàng chính
            var order = _mapper.Map<Orders>(orderDto);
            _repository.Update(order); // Chỉ cập nhật đơn hàng, không đụng tới chi tiết

            // Chèn lại chi tiết mới
            foreach (var detailDto in details)
            {
                var detail = _mapper.Map<Order_Details>(detailDto);
                detail.OrderID = order.ID;
                detail.ID = 0;
                _orderdetailsrepository.Insert(detail);
            }
        }

        //Xóa
        public void Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception($"Order not found with ID = {id}.");        

            _repository.Delete(entity);
        }
        public void DeleteOrderAndDetails(int orderId)
        {
            // Xóa tất cả chi tiết đơn hàng trước
            _orderdetailsrepository.DeleteByOrderID(orderId); // Sử dụng phương thức của OrderDetailsRepository

            // Sau đó, xóa đơn hàng chính
            var orderToDelete = _repository.GetById(orderId);
            if (orderToDelete == null)
            {
                throw new Exception($"Order with ID {orderId} not found for deletion.");
            }
            _repository.Delete(orderToDelete);
        }

        //Hàm mới 
        public List<OrderDTO> GetOrdersByCustomerId(int customerId) {
            if (customerId <= 0)
            {
                throw new ArgumentException("Customer ID must be a positive integer.");
            }
            var orders = _repository.GetByCustomerId(customerId);
            return _mapper.Map<List<OrderDTO>>(orders);
        }
        public List<OrderDTO> GetOrdersByEmployeeId(int employeeId) {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Employee ID must be a positive integer.");
            }
            var orders = _repository.GetByEmployeeId(employeeId);
            return _mapper.Map<List<OrderDTO>>(orders);
        }
    }
}
