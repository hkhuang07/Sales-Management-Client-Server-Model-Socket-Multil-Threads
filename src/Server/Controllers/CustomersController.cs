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
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _unitOfWork.CustomerRepository.GetAll();
            var dtos = _mapper.Map<List<CustomerDTO>>(customers);
            return Ok(new ServerResponse<List<CustomerDTO>>(dtos));
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _unitOfWork.CustomerRepository.GetById(id);
            if (customer != null)
            {
                var dto = _mapper.Map<CustomerDTO>(customer);
                return Ok(new ServerResponse<CustomerDTO>(dto));
            }
            return NotFound(new ServerResponse<CustomerDTO>(null, "Customer not found."));
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] CustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customers>(customerDto);
            _unitOfWork.CustomerRepository.Add(customer);
            _unitOfWork.SaveChanges();
            customerDto.ID = customer.ID;
            return Ok(new ServerResponse<CustomerDTO>(customerDto, "Customer added successfully."));
        }

        [HttpPut]
        public IActionResult UpdateCustomer([FromBody] CustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customers>(customerDto);
            _unitOfWork.CustomerRepository.Update(customer);
            _unitOfWork.SaveChanges();
            return Ok(new ServerResponse<CustomerDTO>(customerDto, "Customer updated successfully."));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _unitOfWork.CustomerRepository.GetById(id);
            if (customer != null)
            {
                _unitOfWork.CustomerRepository.Delete(customer);
                _unitOfWork.SaveChanges();
                return Ok(new ServerResponse<bool>(true, "Customer deleted successfully."));
            }
            return NotFound(new ServerResponse<bool>(false, "Not found"));
        }
    }
}
