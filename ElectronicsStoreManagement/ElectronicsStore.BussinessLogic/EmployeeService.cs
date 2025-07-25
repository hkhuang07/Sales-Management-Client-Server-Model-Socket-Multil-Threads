using System;
using System.Collections.Generic;
using System.Linq;
// using System.Numerics; // Không cần thiết cho mục đích này
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;
using BC = BCrypt.Net.BCrypt; // Đảm bảo alias BC cho BCrypt.Net.BCrypt

namespace ElectronicsStore.BusinessLogic
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IMapper mapper)
        {
            // Trong môi trường thực tế, bạn có thể truyền IEmployeeRepository vào đây
            // để dependency injection linh hoạt hơn.
            _repository = new EmployeeRepository();
            _mapper = mapper;
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

            // Với xác thực, bạn cần đảm bảo password không trống khi thêm mới
            if (isNew && string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Password is required for new employee.");

            // Kiểm tra Role, đảm bảo nó có giá trị boolean hợp lệ (true/false)
            // if (string.IsNullOrEmpty(dto.Role.ToString())) // Role là bool nên không cần kiểm tra thế này
            //     throw new ArgumentException("Role is required.");
        }

        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            var regex = new Regex(@"^(0|\+84)(\d{9})$"); // supports 090xxxxxxx, +849xxxxxxxx
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
                // Không ném exception ở đây mà trả về null để caller xử lý thông báo "Invalid username/password"
                return null;
            }

            var employeeEntity = _repository.GetbyUserName(username); // Sử dụng phương thức từ DAL của bạn

            if (employeeEntity == null)
            {
                return null; // Username not found
            }

            // Verify the entered password against the hashed password from the database
            // BC.Verify(string text, string hash)
            if (BC.Verify(password, employeeEntity.Password))
            {
                return _mapper.Map<EmployeeDTO>(employeeEntity); // Authentication successful
            }

            return null; // Password mismatch
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

        public EmployeeDTO GetByFullName(string fullname)
        {
            // Cẩn thận với GetByFullName, nó có thể trả về nhiều kết quả hoặc không chính xác
            // nếu không có tên đầy đủ duy nhất. Nên dùng GetAll() và lọc ở BLL nếu cần.
            var entity = _repository.GetAll().FirstOrDefault(e => e.FullName == fullname);
            if (entity == null) throw new Exception($"Employee not found with FullName = {fullname}.");
            return _mapper.Map<EmployeeDTO>(entity);
        }

        public EmployeeDTO? GetByUserName(string userName)
        {
            var employee = _repository.GetbyUserName(userName); // DAL
            if (employee == null) return null;
            return _mapper.Map<EmployeeDTO>(employee);
        }


        //Thêm mới
        public void Add(EmployeeDTO dto)
        {
            // Kiểm tra xem username đã tồn tại chưa
            if (_repository.GetbyUserName(dto.UserName) != null)
            {
                throw new ArgumentException($"Username '{dto.UserName}' already exists. Please choose a different username.");
            }

            Validate(dto, isNew: true);
            var entity = _mapper.Map<Employees>(dto);
            // Băm mật khẩu trước khi lưu vào database
            entity.Password = BC.HashPassword(dto.Password);
            _repository.Add(entity);
        }

        //Cập nhật
        public void Update(int id, EmployeeDTO dto)
        {
            Validate(dto, isNew: false);

            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception($"Employee not found with ID = {id}.");

            // Kiểm tra trùng username nếu username bị thay đổi và đã tồn tại
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

            // Optional password update: Chỉ cập nhật password nếu có nhập liệu mới
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                entity.Password = BC.HashPassword(dto.Password);
            }
            // else: Nếu password trống, giữ nguyên password cũ (không thay đổi)

            _repository.Update(entity);
        }

        public void UpdatePassword(int id, string newPassword)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception($"Employee not found with ID = {id}.");

            // Băm mật khẩu mới trước khi cập nhật
            entity.Password = BC.HashPassword(newPassword);
            _repository.Update(entity); // Giả định Update có thể cập nhật password
        }


        //Xóa
        public void Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception($"Employee not found with ID = {id}.");

            _repository.Delete(entity);
        }


        //Các Hàm mới
        // --- Các Hàm mới (Implemented Logic) ---

        public LoginResponseDTO Authentication(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                // Better to throw a specific exception or return an error code/message DTO
                throw new ArgumentException("Username and password cannot be empty.");
            }

            var employeeEntity = _repository.GetbyUserName(username);

            if (employeeEntity == null)
            {
                // For security, avoid specifying if username or password was incorrect.
                // Just say "Invalid credentials".
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            if (BC.Verify(password, employeeEntity.Password))
            {
                // Authentication successful, map to response DTO
                return _mapper.Map<LoginResponseDTO>(employeeEntity);
            }

            throw new UnauthorizedAccessException("Invalid username or password.");
        }


        /* public void RegisterEmployee(RegisterRequestDTO registerRequest)
        {
            ValidateRegisterRequest(registerRequest); // Use a specific validation for registration

            // Check if username already exists
            if (_repository.GetbyUserName(registerRequest.UserName) != null)
            {
                throw new ArgumentException($"Username '{registerRequest.UserName}' already exists. Please choose a different username.");
            }

            // Check if email already exists
            if (_repository.GetbyEmail(registerRequest.Email) != null)
            {
                throw new ArgumentException($"Email '{registerRequest.Email}' already exists. Please use a different email.");
            }

            var newEmployee = _mapper.Map<Employees>(registerRequest);
            newEmployee.Password = BC.HashPassword(registerRequest.Password); // Hash the password
            newEmployee.PasswordResetToken = null; // Ensure these are null for new registrations
            newEmployee.PasswordResetTokenExpiry = null;

            _repository.Add(newEmployee);
        }

        public bool ChangePassword(int employeeId, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("Old password and new password cannot be empty.");
            }
            if (newPassword.Length < 8) // Example: min 8 chars
            {
                throw new ArgumentException("New password must be at least 8 characters long.");
            }

            var employee = _repository.GetById(employeeId);
            if (employee == null)
            {
                throw new Exception($"Employee not found with ID = {employeeId}.");
            }

            // Verify old password
            if (!BC.Verify(oldPassword, employee.Password))
            {
                return false; // Old password does not match
            }

            // Hash and update new password
            employee.Password = BC.HashPassword(newPassword);
            _repository.UpdatePassword(employeeId, employee.Password); // Using the dedicated update password method
            return true;
        }

        public bool RequestPasswordReset(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty.");
            }

            var employee = _repository.GetbyEmail(email);
            if (employee == null)
            {
                // For security, always return true even if email not found,
                // to prevent enumerating valid emails.
                return true;
            }

            // Generate a unique token (e.g., GUID or cryptographically strong random string)
            var token = Guid.NewGuid().ToString("N");
            var expiry = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour

            _repository.SetPasswordResetToken(employee.ID, token, expiry);

            // In a real application, you would send this token to the user's email.
            // Example: _emailService.SendPasswordResetEmail(employee.Email, token);
            Console.WriteLine($"Password reset token for {employee.Email}: {token}"); // For demonstration

            return true;
        }

        public bool ResetPassword(string email, string token, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("Email, token, and new password cannot be empty.");
            }
            if (newPassword.Length < 8) // Example: min 8 chars
            {
                throw new ArgumentException("New password must be at least 8 characters long.");
            }

            var employee = _repository.GetbyEmail(email);
            if (employee == null || employee.PasswordResetToken != token || employee.PasswordResetTokenExpiry == null || employee.PasswordResetTokenExpiry < DateTime.UtcNow)
            {
                // Invalid email, token mismatch, token expired, or no token set
                return false;
            }

            // Hash the new password
            employee.Password = BC.HashPassword(newPassword);

            // Clear the reset token after successful reset
            employee.PasswordResetToken = null;
            employee.PasswordResetTokenExpiry = null;

            _repository.Update(employee); // Use general update, or you could update specifically
                                          // _repository.UpdatePassword(employee.ID, employee.Password);
                                          // _repository.ClearPasswordResetToken(employee.ID);
            return true;
        }
    
    }*/
    }
}
