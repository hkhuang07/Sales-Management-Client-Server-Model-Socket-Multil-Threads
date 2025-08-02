// File: ElectronicsStore.BusinessLogic/OrderService.cs (Đã cập nhật)
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUnitOfWork _unitOfWork; // Thay đổi từ UnitOfWork sang IUnitOfWork để tuân thủ DI

        public OrderService(IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = orderRepository;
            _orderdetailsrepository = orderDetailsRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // --- TRA CỨU ---
        public List<OrderDTO> GetAllList()
        {
            var orders = _repository.GetAllWithDetails();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public OrderDTO? GetById(int id)
        {
            var order = _repository.GetById(id);
            return order != null ? _mapper.Map<OrderDTO>(order) : null;
        }

        // Lọc danh sách theo trạng thái
        public List<OrderDTO> GetByStatus(string status)
        {
            var orders = _repository.GetByStatus(status); // Cần thêm phương thức này vào OrderRepository
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        // --- THÊM MỚI ---
        public void Add(OrderDTO dto)
        {
            if (dto == null) throw new ArgumentException("Order data cannot be null.");
            var entity = _mapper.Map<Orders>(dto);
            _repository.Add(entity);
            _unitOfWork.SaveChanges();
        }

        public int CreateOrder(OrderDTO orderDto, List<OrderDetailsDTO> details)
        {
            using (var transaction = _unitOfWork.BeginTransaction()) // Sử dụng UnitOfWork
            {
                try
                {
                    var order = _mapper.Map<Orders>(orderDto);
                    order.Date = DateTime.Now;

                    _repository.Insert(order);
                    _unitOfWork.SaveChanges(); // Lưu Order trước để có Order.ID

                    foreach (var detailDto in details)
                    {
                        var detail = _mapper.Map<Order_Details>(detailDto);
                        detail.OrderID = order.ID;
                        _orderdetailsrepository.Insert(detail);
                    }

                    _unitOfWork.SaveChanges(); // Lưu OrderDetails
                    transaction.Commit();
                    return order.ID;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }
        public int CreateTmpOrder(OrderDTO orderDto, List<OrderDetailsDTO> details)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var order = _mapper.Map<Orders>(orderDto);
                    order.Date = DateTime.Now;
                    order.Status = "Pending"; // Đảm bảo trạng thái ban đầu là Pending
                    order.CustomerID = 1; // Gán CustomerID = null để tránh lỗi khóa ngoại
                    order.EmployeeID = 1; // Gán EmployeeID = null để tránh lỗi khóa ngoại

                    _repository.Insert(order);
                    _unitOfWork.SaveChanges(); // Lần lưu đầu tiên để lấy Order.ID

                    foreach (var detailDto in details)
                    {
                        var detail = _mapper.Map<Order_Details>(detailDto);
                        detail.OrderID = order.ID;
                        _orderdetailsrepository.Insert(detail);
                    }

                    _unitOfWork.SaveChanges(); // Lưu OrderDetails
                    transaction.Commit();
                    return order.ID;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Ghi lại lỗi chi tiết hơn ở đây để dễ debug
                    Console.WriteLine($"Error in OrderService.CreateOrder: {ex}");
                    throw; // Ném lại lỗi để ServerHandler bắt được
                }
            }
        }
        public void ConfirmOrder(ConfirmOrderDTO dto)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var order = _repository.GetById(dto.OrderID);
                    if (order == null)
                    {
                        throw new InvalidOperationException($"Order with ID {dto.OrderID} not found.");
                    }

                    // Cập nhật các thông tin từ DTO
                    order.CustomerID = dto.CustomerID;
                    order.EmployeeID = dto.EmployeeID;
                    order.Note = dto.Note;
                    order.Status = "Confirmed"; // Cập nhật trạng thái

                    _repository.Update(order);
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

        // --- CẬP NHẬT ---
        public void Update(int id, OrderDTO dto)
        {
            if (dto == null) throw new ArgumentException("Order data cannot be null.");

            var existing = _repository.GetById(id);
            if (existing == null)
                throw new Exception($"Order not found with ID = {id}.");

            _mapper.Map(dto, existing);
            _repository.Update(existing);
            _unitOfWork.SaveChanges(); // Sử dụng UnitOfWork
        }

        // Cập nhật trạng thái của một đơn hàng
        public bool UpdateStatus(int orderId, string newStatus)
        {
            var order = _repository.GetById(orderId);
            if (order == null)
            {
                return false;
            }

            order.Status = newStatus;
            _repository.Update(order);
            _unitOfWork.SaveChanges();
            return true;
        }

        public void UpdateOrder(OrderDTO orderDto, List<OrderDetailsDTO> details)
        {
            using (var transaction = _unitOfWork.BeginTransaction()) // Sử dụng UnitOfWork
            {
                try
                {
                    var existingOrder = _repository.GetById(orderDto.ID);
                    if (existingOrder == null)
                    {
                        throw new Exception($"Order with ID {orderDto.ID} not found for update.");
                    }

                    _mapper.Map(orderDto, existingOrder);
                    _repository.Update(existingOrder);

                    _orderdetailsrepository.DeleteByOrderID(orderDto.ID);

                    if (details != null && details.Any())
                    {
                        var newDetails = _mapper.Map<List<Order_Details>>(details);
                        foreach (var detail in newDetails)
                        {
                            detail.OrderID = existingOrder.ID;
                            _orderdetailsrepository.Insert(detail);
                        }
                    }

                    _unitOfWork.SaveChanges(); // Sử dụng UnitOfWork
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // --- XÓA ---
        public void DeleteOrderAndDetails(int orderId)
        {
            using (var transaction = _unitOfWork.BeginTransaction()) // Sử dụng UnitOfWork
            {
                try
                {
                    _orderdetailsrepository.DeleteByOrderID(orderId);
                    var orderToDelete = _repository.GetById(orderId);
                    if (orderToDelete == null)
                    {
                        throw new Exception($"Order with ID {orderId} not found for deletion.");
                    }
                    _repository.Delete(orderToDelete);

                    _unitOfWork.SaveChanges(); // Sử dụng UnitOfWork
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // Các phương thức khác giữ nguyên
        public List<OrderDTO> GetOrdersByCustomerId(int customerId)
        {
            if (customerId <= 0)
            {
                throw new ArgumentException("Customer ID must be a positive integer.");
            }
            var orders = _repository.GetByCustomerId(customerId);
            return _mapper.Map<List<OrderDTO>>(orders);
        }
        public List<OrderDTO> GetOrdersByEmployeeId(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Employee ID must be a positive integer.");
            }
            var orders = _repository.GetByEmployeeId(employeeId);
            return _mapper.Map<List<OrderDTO>>(orders);
        }


    }


}