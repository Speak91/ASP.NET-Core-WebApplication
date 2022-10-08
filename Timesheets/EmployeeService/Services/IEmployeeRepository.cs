using EmployeeService.Data;

namespace EmployeeService.Services
{
    public interface IEmployeeRepository : IRepository<Employee, int>
    {
    }
}
