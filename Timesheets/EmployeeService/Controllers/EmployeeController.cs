using EmployeeService.Models;
using EmployeeService.Models.Requests.Employee;
using EmployeeService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Services

        private readonly IEmployeeRepository _employeeRepository;

        #endregion

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost("create")]
        public ActionResult Create([FromBody] CreateEmployeeRequest request)
        {
            return Ok(_employeeRepository.Create(new Employee
            {
                DepartmentId = request.DepartmentId,
                EmployeeTypeId = request.EmployeeTypeId,
                FirstName = request.FirstName,
                Patronymic = request.Patronymic,
                Salary = request.Salary,
                Surname = request.Surname
            }));
        }

        [HttpPut("Update")]
        public ActionResult Update([FromBody] UpdateEmployee request)
        {
            return Ok(_employeeRepository.Create(new Employee
            {
                DepartmentId = request.DepartmentId,
                EmployeeTypeId = request.EmployeeTypeId,
                FirstName = request.FirstName,
                Patronymic = request.Patronymic,
                Salary = request.Salary,
                Surname = request.Surname
            }));
        }

        [HttpGet("get/all")]
        public ActionResult<List<Employee>> GetAllEmployees()
        {
            return Ok(_employeeRepository.GetAll());
        }

        [HttpGet("get/{id}")]
        public ActionResult<Employee> GetById([FromRoute] int id)
        {
            var employee = _employeeRepository.GetById(id);
            return Ok(new Employee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                DepartmentId = employee.DepartmentId,
                EmployeeTypeId = employee.EmployeeTypeId,
                Salary = employee.Salary,
                Patronymic = employee.Patronymic,
                Surname = employee.Surname
            });
        }

        [HttpDelete("delete")]
        public ActionResult Delete([FromRoute] int id)
        {
            _employeeRepository.Delete(id);
            return Ok();
        }
    }
}
