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
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            var dtos = _mapper.Map<List<EmployeeDTO>>(employees);
            return Ok(new ServerResponse<List<EmployeeDTO>>(dtos));
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee != null)
            {
                var dto = _mapper.Map<EmployeeDTO>(employee);
                return Ok(new ServerResponse<EmployeeDTO>(dto));
            }
            return NotFound(new ServerResponse<EmployeeDTO>(null, "Employee not found."));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddEmployee([FromBody] EmployeeDTO employeeDto)
        {
            var employee = _mapper.Map<Employees>(employeeDto);
            employee.Password = BCrypt.Net.BCrypt.HashPassword(employee.Password);
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.SaveChanges();
            employeeDto.ID = employee.ID;
            return Ok(new ServerResponse<EmployeeDTO>(employeeDto, "Added successfully."));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateEmployee([FromBody] EmployeeDTO employeeDto)
        {
            var employee = _mapper.Map<Employees>(employeeDto);
            
            var existing = _unitOfWork.EmployeeRepository.GetById(employee.ID);
            if (existing != null)
            {
                existing.FullName = employee.FullName;
                existing.Role = employee.Role;
                existing.UserName = employee.UserName;
                existing.EmployeePhone = employee.EmployeePhone;
                existing.EmployeeAddress = employee.EmployeeAddress;
                
                if (!string.IsNullOrEmpty(employeeDto.Password))
                {
                    existing.Password = BCrypt.Net.BCrypt.HashPassword(employeeDto.Password);
                }
                
                _unitOfWork.EmployeeRepository.Update(existing);
                _unitOfWork.SaveChanges();
                return Ok(new ServerResponse<EmployeeDTO>(employeeDto, "Updated successfully."));
            }
            return NotFound(new ServerResponse<bool>(false, "Not found"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee != null)
            {
                _unitOfWork.EmployeeRepository.Delete(employee);
                _unitOfWork.SaveChanges();
                return Ok(new ServerResponse<bool>(true, "Deleted successfully."));
            }
            return NotFound(new ServerResponse<bool>(false, "Not found"));
        }
    }
}
