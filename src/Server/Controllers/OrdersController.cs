using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _unitOfWork.OrderRepository.GetAll();
            var dtos = _mapper.Map<List<OrderDTO>>(orders);
            return Ok(new ServerResponse<List<OrderDTO>>(dtos));
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _unitOfWork.OrderRepository.GetById(id);
            if (order != null)
            {
                var dto = _mapper.Map<OrderDTO>(order);
                return Ok(new ServerResponse<OrderDTO>(dto));
            }
            return NotFound(new ServerResponse<OrderDTO>(null, "Order not found."));
        }

        [HttpGet("{id}/details")]
        public IActionResult GetOrderDetails(int id)
        {
            var details = _unitOfWork.OrderDetailsRepository.GetByOrderID(id);
            var dtos = _mapper.Map<List<OrderDetailsDTO>>(details);
            return Ok(new ServerResponse<List<OrderDetailsDTO>>(dtos));
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] OrderWithDetailsDTO request)
        {
            var order = _mapper.Map<Orders>(request.Order);
            _unitOfWork.OrderRepository.Add(order);
            _unitOfWork.SaveChanges(); // Save order first to get generated ID

            foreach (var od in request.OrderDetails)
            {
                var detail = _mapper.Map<Order_Details>(od);
                detail.OrderID = order.ID; 
                _unitOfWork.OrderDetailsRepository.Insert(detail);
                
                var product = _unitOfWork.ProductRepository.GetById(od.ProductID);
                if (product != null)
                {
                    product.Quantity -= od.Quantity;
                    _unitOfWork.ProductRepository.Update(product);
                }
            }

            _unitOfWork.SaveChanges(); // Commit details and product stock updates

            return Ok(new ServerResponse<int>(order.ID, "Order added successfully."));
        }
        
        [HttpPut]
        public IActionResult UpdateOrder([FromBody] OrderDTO orderDto)
        {
            var existingOrder = _unitOfWork.OrderRepository.GetById(orderDto.ID);
            if (existingOrder == null)
            {
                return NotFound(new ServerResponse<bool>(false, "Order not found."));
            }

            if (orderDto.CustomerID > 0) existingOrder.CustomerID = orderDto.CustomerID;
            if (orderDto.EmployeeID > 0) existingOrder.EmployeeID = orderDto.EmployeeID;
            if (!string.IsNullOrEmpty(orderDto.Note)) existingOrder.Note = orderDto.Note;
            if (!string.IsNullOrEmpty(orderDto.Status)) existingOrder.Status = orderDto.Status;
            if (orderDto.Date != default) existingOrder.Date = orderDto.Date;

            _unitOfWork.OrderRepository.Update(existingOrder);
            _unitOfWork.SaveChanges();
            return Ok(new ServerResponse<bool>(true, "Order updated successfully."));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _unitOfWork.OrderRepository.GetById(id);
            if (order != null)
            {
                var details = _unitOfWork.OrderDetailsRepository.GetByOrderID(id);
                foreach (var detail in details)
                {
                    _unitOfWork.OrderDetailsRepository.Delete(detail);
                }
                
                _unitOfWork.OrderRepository.Delete(order);
                _unitOfWork.SaveChanges();
                return Ok(new ServerResponse<bool>(true, "Order deleted successfully."));
            }
            return NotFound(new ServerResponse<bool>(false, "Not found"));
        }
    }
}
