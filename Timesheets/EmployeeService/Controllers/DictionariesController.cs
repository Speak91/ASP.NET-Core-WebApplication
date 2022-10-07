using EmployeeService.Models.Requests.EmployeeType;
using EmployeeService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionariesController : ControllerBase
    {
        private readonly IEmployeeTypeRepository _employeeTypeRepository;

        public DictionariesController(IEmployeeTypeRepository employeeTypeRepository)
        {
            _employeeTypeRepository = employeeTypeRepository;
        }

        [HttpPut("employee-types/add")]
        public IActionResult AddEmployeeType(CreateEmployeeType employeeType)
        {
            return Ok(_employeeTypeRepository.Create(new Models.EmployeeType
            {
                Id = employeeType.Id,
                Descritption = employeeType.Descritption
            }));
        }

        [HttpPut("employee-types/update")]
        public IActionResult UpdateEmployeeType(UpdateEmployeeType employeeType)
        {
            return Ok(_employeeTypeRepository.Create(new Models.EmployeeType
            {
                Id = employeeType.Id,
                Descritption = employeeType.Descritption
            }));
        }

        [HttpGet("employee-types/all")]
        public IActionResult GetAllEmployeeTypes()
        {
            return Ok(_employeeTypeRepository.GetAll());
        }

        [HttpGet("employee-types/getById")]
        public IActionResult GetEmployeeTypeById(int id)
        {
            return Ok(_employeeTypeRepository.GetById(id));
        }

        [HttpGet("employee-types/delete")]
        public IActionResult DeleteEmployeeType(int id)
        {
            _employeeTypeRepository.Delete(id);
            return Ok();
        }
    }
}
