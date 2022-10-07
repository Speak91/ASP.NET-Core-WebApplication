using EmployeeService.Models;

namespace EmployeeService.Services.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        int IRepository<Employee, int>.Create(Employee data)
        {
            throw new NotImplementedException();
        }

        void IRepository<Employee, int>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        IList<Employee> IRepository<Employee, int>.GetAll()
        {
            throw new NotImplementedException();
        }

        Employee IRepository<Employee, int>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        void IRepository<Employee, int>.Update(Employee data)
        {
            throw new NotImplementedException();
        }
    }
}
