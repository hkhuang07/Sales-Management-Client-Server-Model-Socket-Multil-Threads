using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicsStore.DataAccess; // Assuming Employees is in DataAccess
using System.Collections.Generic;

namespace ElectronicsStore.DataAccess
{
    public interface IEmployeeRepository
    {

        List<Employees> GetAll();
        Employees? GetById(int id);
        Employees? GetbyUserName(string userName);

        void Add(Employees employee);
        void Update(Employees employee);
        void UpdatePassword(int id, string hashedPassword);
        void Delete(Employees employee);

        /* New methods specific to password reset token management
        Employees? GetbyEmail(string email); // New method for password reset
        void SetPasswordResetToken(int employeeId, string token, DateTime expiry);
        void ClearPasswordResetToken(int employeeId);*/
    }
}