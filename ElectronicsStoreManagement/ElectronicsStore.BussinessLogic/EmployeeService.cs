using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;
using BC = BCrypt.Net.BCrypt;

namespace ElectronicsStore.BusinessLogic
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        // Constructor đã được cập nhật để nhận IEmployeeRepository qua DI
        public EmployeeService(IEmployeeRepository repository, IMapper mapper, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //Kiểm tra dữ liệu đầu vào
        private void Validate(EmployeeDTO dto, bool isNew = true)
        {
            if (dto == null)
                throw new ArgumentException("Employee data cannot be null.");

            if (string.IsNullOrWhiteSpace(dto.FullName) || dto.FullName.Length > 200)
                throw new ArgumentException("Employee name cannot be blank and must be max 200 characters.");

            if (!IsValidUserName(dto.UserName))
                throw new ArgumentException("Username must be 6–32 characters long, letters, digits or underscores only.");

            if (string.IsNullOrWhiteSpace(dto.EmployeeAddress) || dto.EmployeeAddress.Length > 200)
                throw new ArgumentException("Address cannot be blank and must be max 200 characters.");

            if (!IsValidPhone(dto.EmployeePhone))
                throw new ArgumentException("Invalid phone number. Example: 0901234567.");

            if (isNew && string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Password is required for new employee.");
        }

        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            var regex = new Regex(@"^(0|\+84)(\d{9})$");
            return regex.IsMatch(phone);
        }

        private bool IsValidUserName(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            var regex = new Regex(@"^[a-zA-Z0-9_]{6,32}$");
            return regex.IsMatch(username);
        }

        // --- AUTHENTICATION METHOD ---
        public EmployeeDTO Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var employeeEntity = _repository.GetbyUserName(username);

            if (employeeEntity == null)
            {
                return null;
            }

            if (BC.Verify(password, employeeEntity.Password))
            {
                return _mapper.Map<EmployeeDTO>(employeeEntity);
            }

            return null;
        }

        //Tra cứu
        public List<EmployeeDTO> GetAll()
        {
            var list = _repository.GetAll();
            return _mapper.Map<List<EmployeeDTO>>(list);
        }

        public EmployeeDTO GetById(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new Exception($"Employee not found with ID = {id}.");
            return _mapper.Map<EmployeeDTO>(entity);
        }

        public List<EmployeeDTO> GetByFullName(string fullname)
        {
            var list = _repository.GetAll().Where(e => e.FullName.Contains(fullname)).ToList();
            return _mapper.Map<List<EmployeeDTO>>(list);

        }

        public EmployeeDTO? GetByUserName(string userName)
        {
            var employee = _repository.GetbyUserName(userName);
            if (employee == null) return null;
            return _mapper.Map<EmployeeDTO>(employee);
        }

        //Thêm mới
        public void Add(EmployeeDTO dto)
        {
            if (_repository.GetbyUserName(dto.UserName) != null)
            {
                throw new ArgumentException($"Username '{dto.UserName}' already exists. Please choose a different username.");
            }

            Validate(dto, isNew: true);
            var entity = _mapper.Map<Employees>(dto);
            entity.Password = BC.HashPassword(dto.Password);
            _repository.Add(entity);
            _unitOfWork.SaveChanges();

        }

        //Cập nhật
        public void Update(int id, EmployeeDTO dto)
        {
            Validate(dto, isNew: false);

            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception($"Employee not found with ID = {id}.");

            if (entity.UserName != dto.UserName)
            {
                if (_repository.GetbyUserName(dto.UserName) != null)
                {
                    throw new ArgumentException($"Username '{dto.UserName}' already exists. Please choose a different username.");
                }
            }

            entity.FullName = dto.FullName;
            entity.UserName = dto.UserName;
            entity.EmployeeAddress = dto.EmployeeAddress;
            entity.EmployeePhone = dto.EmployeePhone;
            entity.Role = dto.Role;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                entity.Password = BC.HashPassword(dto.Password);
            }

            _repository.Update(entity);
            _unitOfWork.SaveChanges();

        }

        public void UpdatePassword(int id, string newPassword)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception($"Employee not found with ID = {id}.");

            entity.Password = BC.HashPassword(newPassword);
            _repository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        //Xóa
        public void Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception($"Employee not found with ID = {id}.");

            _repository.Delete(entity);
            _unitOfWork.SaveChanges();
        }

        //Các Hàm mới
        public LoginResponseDTO Authentication(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }

            var employeeEntity = _repository.GetbyUserName(username);

            if (employeeEntity == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            if (BC.Verify(password, employeeEntity.Password))
            {
                return _mapper.Map<LoginResponseDTO>(employeeEntity);
            }

            throw new UnauthorizedAccessException("Invalid username or password.");
        }
    }
}