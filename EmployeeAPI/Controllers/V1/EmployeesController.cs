using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.Domain;
using EmployeeAPI.Contracts.V1;
using EmployeeAPI.Contracts.V1.Requests;
using EmployeeAPI.Contracts.V1.Responses;
using EmployeeAPI.Services;
using EmployeeAPI.Mappings.V1;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace EmployeeAPI.Controllers.V1
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<WeatherForecastController> _logger;

        public EmployeesController(IEmployeeService employeeService, ILogger<WeatherForecastController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet(ApiRoutes.Employees.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting employees");
            return Ok(await _employeeService.GetEmployeesAsync());
        }

        [HttpGet(ApiRoutes.Employees.GetAllByBossId)]
        public async Task<IActionResult> GetAllByBossId([FromRoute] int bossId)
        {
            _logger.LogInformation("Getting employees");
            return Ok(await _employeeService.GetEmployeesByBossIdAsync(bossId));
        }

        [HttpGet(ApiRoutes.Employees.GetAllByNameAndBirthInterval)]
        public async Task<IActionResult> GetAllByNameAndBirthInterval([FromRoute] string firstName, [FromRoute] DateTime intervalStart, [FromRoute] DateTime intervalEnd)
        {
            _logger.LogInformation("Getting employees");
            return Ok(await _employeeService.GetEmployeesByNameAndBirthIntervalIdAsync(firstName, intervalStart, intervalEnd));
        }

        [HttpGet(ApiRoutes.Employees.GetCountAndAverageSalary)]
        public async Task<IActionResult> GetCountAndAverageSalary([FromRoute] string role)
        {
            _logger.LogInformation("Getting count and average salary");
            var result = await _employeeService.GetCountAndAverageSalary(role);
            var averageSalary = new AverageSalaryRequest { Count = result.count, AverageSalary = result.averageSalary };
            return Ok(averageSalary);
        }

        [HttpPut(ApiRoutes.Employees.Update)]
        public async Task<IActionResult> Update([FromRoute] int employeeId, [FromBody] UpdateEmployeeRequest request)
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

        [HttpPatch(ApiRoutes.Employees.UpdateSalary)]
        public async Task<IActionResult> UpdateSalary([FromRoute] int employeeId, int salary)
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

        [HttpDelete(ApiRoutes.Employees.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int employeeId)
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

        [HttpGet(ApiRoutes.Employees.Get)]
        public async Task<IActionResult> Get([FromRoute] int employeeId)
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

        [HttpPost(ApiRoutes.Employees.Create)]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest employeeRequest)
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

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                var locationUri = baseUrl + "/" + ApiRoutes.Employees.Get.Replace("{employeeId}", employee.Id.ToString());

                var response = EmployeeMappings.ToEmployeeResponse(employee);

                return Created(locationUri, response);
            }
            return BadRequest(new { error = "BossId not found" });
        }
    }
}


