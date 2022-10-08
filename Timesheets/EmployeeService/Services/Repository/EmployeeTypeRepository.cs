using EmployeeService.Data;

namespace EmployeeService.Services.Repository
{
    public class EmployeeTypeRepository : IEmployeeTypeRepository
    {
        int IRepository<EmployeeType, int>.Create(EmployeeType data)
        {
            throw new NotImplementedException();
        }

        void IRepository<EmployeeType, int>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        IList<EmployeeType> IRepository<EmployeeType, int>.GetAll()
        {
            throw new NotImplementedException();
        }

        EmployeeType IRepository<EmployeeType, int>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        void IRepository<EmployeeType, int>.Update(EmployeeType data)
        {
            throw new NotImplementedException();
        }
    }
}
