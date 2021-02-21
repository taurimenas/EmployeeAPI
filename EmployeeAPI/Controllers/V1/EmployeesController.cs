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

namespace EmployeeAPI.Controllers.V1
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet(ApiRoutes.Employees.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeService.GetEmployeesAsync());
        }

        [HttpPut(ApiRoutes.Employees.Update)]
        public async Task<IActionResult> Update([FromRoute] int employeeId, [FromBody] UpdateEmployeeRequest request)
        {
            var employees = await _employeeService.GetEmployeesAsync();
            var employeeToUpdate = await _employeeService.GetEmployeeByIdAsync(employeeId);
            var employee = EmployeeMappings.ToEmployee(employeeToUpdate, request, employees);

            var updated = await _employeeService.UpdateEmployeeAsync(employee);

            if (updated)
            {
                return Ok(employee);
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Employees.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int employeeId)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(employeeId);

            if (deleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet(ApiRoutes.Employees.Get)]
        public async Task<IActionResult> Get([FromRoute] int employeeId)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost(ApiRoutes.Employees.Create)]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest employeeRequest)
        {
            var employees = await _employeeService.GetEmployeesAsync();
            if (employeeRequest.Role == "CEO" && employees.FindAll(x => x.Role == "CEO").Count > 0)
            {
                return BadRequest(new { error = "There can be only 1 employee with CEO role." });
            }

            var employee = EmployeeMappings.ToEmployee(employeeRequest, employees);

            var created = await _employeeService.CreateEmployeeAsync(employee);

            if (!created)
            {
                return BadRequest(new { error = "Unable to create employee" });
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Employees.Get.Replace("{employeeId}", employee.Id.ToString());

            var response = EmployeeMappings.ToEmployeeResponse(employee); 

            return Created(locationUri, response);
        }
    }
}
