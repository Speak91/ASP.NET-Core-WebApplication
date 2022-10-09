using EmployeeService.Data;
using EmployeeService.Models;

namespace EmployeeService.Services.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeServiceDbContext _context;

        public DepartmentRepository(EmployeeServiceDbContext context)
        {
            _context = context;
        }

        public Guid Create(Department data)
        {
            _context.Departments.Add(data);
            _context.SaveChanges();
            return data.Id;
        }

        public void Delete(Guid id)
        {
            Department department  = GetById(id);
            if (department == null)
            {
                throw new Exception("departmentNotFound");
            }
            _context.Departments.Remove(department);
            _context.SaveChanges();
        }

        public IList<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department GetById(Guid id)
        {
            return _context.Departments.FirstOrDefault(e => e.Id == id);
        }

        public void Update(Department data)
        {
            if (data == null)
                throw new Exception("Department is Null");
            Department department = GetById(data.Id);

            department.Description = data.Description;
            _context.SaveChanges();
        }
    }
}
