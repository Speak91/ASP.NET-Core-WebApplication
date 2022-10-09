using EmployeeService.Data;
using EmployeeService.Models;

namespace EmployeeService.Services.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeServiceDbContext _context;

        public EmployeeRepository(EmployeeServiceDbContext context)
        {
            _context = context;
        }

        public int Create(Employee data)
        {
            _context.Employees.Add(data);
            _context.SaveChanges();
            return data.Id;
        }

        public void Delete(int id)
        {
            Employee employee = GetById(id);
            if (employee == null)
            {
                throw new Exception("EmployeeType");
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public IList<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public void Update(Employee data)
        {
            if (data == null)
                throw new Exception("EmployeesType is Null");
            Employee employee = GetById(data.Id);

            employee = data;
            _context.SaveChanges();
        }
    }
}
