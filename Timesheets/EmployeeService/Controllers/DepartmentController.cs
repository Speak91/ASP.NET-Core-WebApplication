using EmployeeService.Data;
using EmployeeService.Models.Dto;
using EmployeeService.Models.Options;
using EmployeeService.Models.Requests.Department;
using EmployeeService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmployeeService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IOptions<LoggerOptions> _loggerOptions;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(
            IDepartmentRepository departmentRepository,
            IOptions<LoggerOptions> loggerOptions,
            ILogger<DepartmentController> logger)
        {
            _loggerOptions = loggerOptions;
            _departmentRepository = departmentRepository;
            _logger = logger;
        }

        [HttpPost("add")]
        public ActionResult AddDepartment(CreateDepartmentRequest department)
        {
            _logger.LogInformation("Department add");
            return Ok(_departmentRepository.Create(new Department
            {
                Description = department.Description
            }));
        }

        [HttpGet("all")]
        public ActionResult<List<DepartmentDto>> GetAllDepartments()
        {
            _logger.LogInformation("Departments transferred");
            return Ok(_departmentRepository.GetAll().Select(department=> new DepartmentDto
            {
                Id = department.Id,
                Description = department.Description
            }).ToList());
        }

        [HttpPut("update")]
        public ActionResult UpdateDepartment(UpdateDepartmentRequest department)
        {
            _logger.LogInformation("Departments updated");
            _departmentRepository.Update(new Department
            {
                Description = department.Description,
                Id = department.Id
            });
            return Ok();
        }

        [HttpGet("getById")]
        public ActionResult<DepartmentDto> GetById(Guid id)
        {
            _logger.LogInformation("Department transferred");
            var department = _departmentRepository.GetById(id);
            return Ok(new DepartmentDto
            {
                Id=department.Id,
                Description = department.Description
            });
        }

        [HttpDelete("delete")]
        public ActionResult Delete(Guid id)
        {
            _logger.LogInformation("Department deleted");
            _departmentRepository.Delete(id);
            return Ok();
        }
    }
}
