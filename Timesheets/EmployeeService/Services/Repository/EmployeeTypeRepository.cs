using EmployeeService.Data;

namespace EmployeeService.Services.Repository
{
    public class EmployeeTypeRepository : IEmployeeTypeRepository
    {
        private readonly EmployeeServiceDbContext _context;

        public EmployeeTypeRepository(EmployeeServiceDbContext context)
        {
            _context = context;
        }
        public int Create(EmployeeType data)
        {
            _context.EmployeeTypes.Add(data);
            _context.SaveChanges();
            return data.Id;
        }

        public void Delete(int id)
        {
            EmployeeType employeeType = GetById(id);
            if (employeeType == null)
            {
                throw new Exception("EmployeeTypeNotFound");
            }
            _context.EmployeeTypes.Remove(employeeType);
            _context.SaveChanges();
        }

        public IList<EmployeeType> GetAll()
        {
            return _context.EmployeeTypes.ToList();
        }

        public EmployeeType GetById(int id)
        {
            return _context.EmployeeTypes.FirstOrDefault(e => e.Id == id);
        }

        public void Update(EmployeeType data)
        {
            if (data == null)
                throw new Exception("EmployeesType is Null");
            EmployeeType employeeType = GetById(data.Id);

            employeeType.Description = data.Description;
            _context.SaveChanges();
        }
    }
}
