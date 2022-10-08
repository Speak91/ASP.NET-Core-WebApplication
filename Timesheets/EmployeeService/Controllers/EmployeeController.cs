using EmployeeService.Data;
using EmployeeService.Models;
using EmployeeService.Models.Dto;
using EmployeeService.Models.Options;
using EmployeeService.Models.Requests.Employee;
using EmployeeService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOptions<LoggerOptions> _loggerOptions;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepository, 
            IOptions<LoggerOptions> loggerOptions,
             ILogger<EmployeeController> logger)
        {
            _loggerOptions = loggerOptions;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public ActionResult Create([FromBody] CreateEmployeeRequest request)
        {
            _logger.LogInformation("Employee added");
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
            _logger.LogInformation("Employee updated");
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
        public ActionResult<List<EmployeeDto>> GetAllEmployees()
        {
            _logger.LogInformation("Employees transferred");
            return Ok(_employeeRepository.GetAll().Select(employee=> new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                DepartmentId = employee.DepartmentId,
                EmployeeTypeId = employee.EmployeeTypeId,
                Salary = employee.Salary,
                Patronymic = employee.Patronymic,
                Surname = employee.Surname
            }).ToList());
        }

        [HttpGet("get/{id}")]
        public ActionResult<EmployeeDto> GetById([FromRoute] int id)
        {
            _logger.LogInformation("Employee transferred");
            var employee = _employeeRepository.GetById(id);
            return Ok(new EmployeeDto
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
            _logger.LogInformation("Employee deleted");
            _employeeRepository.Delete(id);
            return Ok();
        }
    }
}
