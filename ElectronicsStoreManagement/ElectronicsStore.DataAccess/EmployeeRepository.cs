using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataAccess
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly ElectronicsStoreContext _context;

        public EmployeeRepository()
        {
            _context = new ElectronicsStoreContext();
        }

        // Tra cứu
        public List<Employees> GetAll() => _context.Employee.ToList();

        public Employees? GetById(int id) => _context.Employee.Find(id);

        public Employees? GetbyUserName(string userName)
        {
            return _context.Employee.FirstOrDefault(e => e.UserName == userName);
        }
        /*public Employees? GetbyEmail(string email) // NEW: Implementation for GetbyEmail
        {
            return _context.Employee.FirstOrDefault(e => e. == email); // Assuming Employees entity has an 'Email' property
        }*/

        //Thêm mới
        public void Add(Employees employee)
        {
            _context.Employee.Add(employee);
            _context.SaveChanges();
        }

        //Cập nhật
        public void Update(Employees employee)
        {
            var existingEmployee = _context.Employee.Find(employee.ID);
            if (existingEmployee != null)
            {
                // Cập nhật các thuộc tính cần thiết
                existingEmployee.FullName = employee.FullName;
                existingEmployee.EmployeeAddress = employee.EmployeeAddress;
                existingEmployee.EmployeePhone = employee.EmployeePhone;
                existingEmployee.Role = employee.Role;
                existingEmployee.Password = employee.Password;

                _context.Employee.Update(existingEmployee);
                _context.SaveChanges();
                _context.Entry(employee).Reload();

            }
            else
            {
                throw new Exception($"Employee with ID = {employee.ID} not found.");
            }

        }

        public void UpdatePassword(int id, string hashedPassword)
        {
            var employee = _context.Employee.Find(id);
            if (employee != null)   
            {
                employee.Password = hashedPassword;
                _context.SaveChanges();
            }
            _context.Entry(employee).Reload();

        }

        //Xóa
        public void Delete(Employees employee)
        {
            _context.Employee.Remove(employee);
            _context.SaveChanges();
        }

        /*public void SetPasswordResetToken(int employeeId, string token, DateTime expiry)
        {
            var employee = _context.Employee.Find(employeeId);
            if (employee != null)
            {
                employee.PasswordResetToken = token;
                employee.PasswordResetTokenExpiry = expiry;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Employee with ID = {employeeId} not found to set password reset token.");
            }
        }

        public void ClearPasswordResetToken(int employeeId)
        {
            var employee = _context.Employee.Find(employeeId);
            if (employee != null)
            {
                employee.PasswordResetToken = null;
                employee.PasswordResetTokenExpiry = null;
                _context.SaveChanges();
            }
            // No error if not found, as it might already be cleared or employee doesn't exist
        }*/
    }
}
