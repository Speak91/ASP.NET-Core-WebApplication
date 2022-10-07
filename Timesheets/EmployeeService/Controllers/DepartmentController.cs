using EmployeeService.Models;
using EmployeeService.Models.Requests.Department;
using EmployeeService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(
            IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet("all")]
        public IActionResult GetAllDepartments()
        {
            return Ok(_departmentRepository.GetAll());
        }

        [HttpPost("add")]
        public IActionResult AddDepartment(CreateDepartmentRequest department)
        {
            return Ok(_departmentRepository.Create(new Department
            {
                Description = department.Description,
                Id = department.Id
            }));
        }

        [HttpPut("update")]
        public IActionResult UpdateDepartment(UpdateDepartmentRequest department)
        {
            return Ok(_departmentRepository.Create(new Department
            {
                Description = department.Description,
                Id = department.Id
            }));
        }

        [HttpGet("getById")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_departmentRepository.GetById(id));
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Guid id)
        {
            _departmentRepository.Delete(id);
            return Ok();
        }
    }
}
