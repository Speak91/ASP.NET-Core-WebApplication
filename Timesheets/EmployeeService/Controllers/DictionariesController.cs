using EmployeeService.Data;
using EmployeeService.Models.Dto;
using EmployeeService.Models.Options;
using EmployeeService.Models.Requests.EmployeeType;
using EmployeeService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionariesController : ControllerBase
    {
        private readonly IEmployeeTypeRepository _employeeTypeRepository;
        private readonly IOptions<LoggerOptions> _loggerOptions;
        private readonly ILogger<DictionariesController> _logger;
        public DictionariesController(IEmployeeTypeRepository employeeTypeRepository, 
            ILogger<DictionariesController> logger, 
            IOptions<LoggerOptions> loggerOptions)
        {
            _employeeTypeRepository = employeeTypeRepository;
            _loggerOptions = loggerOptions;
            _logger = logger;
        }

        [HttpPut("employee-types/add")]
        public ActionResult AddEmployeeType(CreateEmployeeType employeeType)
        {
            _logger.LogInformation("EmployeeType added");
            return Ok(_employeeTypeRepository.Create(new EmployeeType
            {
                Description = employeeType.Descritption
            }));
        }

        [HttpPut("employee-types/update")]
        public ActionResult UpdateEmployeeType(UpdateEmployeeType employeeType)
        {
            _logger.LogInformation("EmployeeType updated");
            return Ok(_employeeTypeRepository.Create(new EmployeeType
            {
                Id = employeeType.Id,
                Description = employeeType.Descritption
            }));
        }

        [HttpGet("employee-types/all")]
        public ActionResult<List<EmployeeTypeDto>> GetAllEmployeeTypes()
        {
            _logger.LogInformation("EmpployeeTypes transfered");
            return Ok(_employeeTypeRepository.GetAll().Select(employeeType => new EmployeeTypeDto
            {
                Id = employeeType.Id,
                Descritption = employeeType.Description
            }));
        }

        [HttpGet("employee-types/getById")]
        public ActionResult<EmployeeTypeDto> GetEmployeeTypeById(int id)
        {
            _logger.LogInformation("EmpployeeType transfered");
            var employeeType = _employeeTypeRepository.GetById(id);
            return Ok(new EmployeeTypeDto
            {
                Id = employeeType.Id,
                Descritption = employeeType.Description
            });
        }

        [HttpGet("employee-types/delete")]
        public ActionResult DeleteEmployeeType(int id)
        {
            _logger.LogInformation("EmployeeType deleted");
            _employeeTypeRepository.Delete(id);
            return Ok();
        }
    }
}
