using EmployeeService.Models;

namespace EmployeeService.Services.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        int IRepository<Department, Guid>.Create(Department data)
        {
            throw new NotImplementedException();
        }

        void IRepository<Department, Guid>.Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        IList<Department> IRepository<Department, Guid>.GetAll()
        {
            throw new NotImplementedException();
        }

        Department IRepository<Department, Guid>.GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        void IRepository<Department, Guid>.Update(Department data)
        {
            throw new NotImplementedException();
        }
    }
}
