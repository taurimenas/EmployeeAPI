using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeAPI.Contracts.V1;
using EmployeeAPI.Contracts.V1.Requests;
using EmployeeAPI.Services;
using EmployeeAPI.Mappings.V1;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace EmployeeAPI.Controllers.V1
{
    [Route("api/v1/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] EmployeeQueryModel employeeQueryModel)
        {
            _logger.LogInformation("Getting employees");
            return Ok(await _employeeService.GetEmployeesAsync(employeeQueryModel));
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> Get(int employeeId)
        {
            _logger.LogInformation("Getting employee {Id}", employeeId);

            var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);

            if (employee is null)
            {
                _logger.LogWarning("Get employee ({Id}) NOT FOUND", employeeId);
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet("role")]
        public async Task<IActionResult> GetCountAndAverageSalary([FromQuery] string role)
        {
            _logger.LogInformation("Getting count and average salary");
            var result = await _employeeService.GetCountAndAverageSalaryAsync(role);
            var averageSalary = new AverageSalaryRequest { Count = result.count, AverageSalary = result.averageSalary };
            return Ok(averageSalary);
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> Update(int employeeId, EmployeeRequest request)
        {
            _logger.LogInformation("Getting employees");
            var employees = await _employeeService.GetEmployeesAsync();
            _logger.LogInformation("Getting employee {Id}", employeeId);
            var employeeToUpdate = await _employeeService.GetEmployeeByIdAsync(employeeId);
            var employee = EmployeeMappings.ToEmployee(employeeToUpdate, request, employees);

            var updated = await _employeeService.UpdateEmployeeAsync(employee);

            if (updated)
            {
                return Ok(employee);
            }

            return NotFound();
        }

        [HttpPatch("{employeeId}")]
        public async Task<IActionResult> UpdateSalary(int employeeId, int salary)
        {
            _logger.LogInformation("Getting employee {Id}", employeeId);
            var employeeToUpdate = await _employeeService.GetEmployeeByIdAsync(employeeId);
            var updated = await _employeeService.UpdateEmployeeSalaryAsync(employeeToUpdate, salary);

            if (updated)
            {
                return Ok(employeeToUpdate);
            }

            return NotFound();
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> Delete(int employeeId)
        {
            _logger.LogInformation("Getting employee {Id}", employeeId);
            var deleted = await _employeeService.DeleteEmployeeAsync(employeeId);

            if (deleted)
            {
                _logger.LogWarning("Deleted employee ({Id})", employeeId);
                return NoContent();
            }

            _logger.LogWarning("Delete employee ({Id}) NOT FOUND", employeeId);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeRequest employeeRequest)
        {
            _logger.LogInformation("Getting employees");
            var employees = await _employeeService.GetEmployeesAsync();
            if (employeeRequest.Role == "CEO" && employees.FindAll(x => x.Role == "CEO").Count > 0)
            {
                _logger.LogWarning("There can be only 1 employee with CEO role.");
                return BadRequest(new { error = "There can be only 1 employee with CEO role" });
            }
            if (!(employees.Find(x => x.Id == employeeRequest.BossId) is null))
            {
                var employee = EmployeeMappings.ToEmployee(employeeRequest, employees);

                var created = await _employeeService.CreateEmployeeAsync(employee);

                if (!created)
                {
                    _logger.LogWarning("Unable to create employee");
                    return BadRequest(new { error = "Unable to create employee" });
                }

                var response = EmployeeMappings.ToEmployeeResponse(employee);

                return CreatedAtAction(nameof(GetAll), new { id = employee.Id }, response);
            }
            return BadRequest(new { error = "BossId not found" });
        }
    }
}


