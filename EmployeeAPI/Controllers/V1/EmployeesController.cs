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

namespace EmployeeAPI.Controllers.V1
{
    public class EmployeesController : Controller
    {
        private IEmployeeService _employeeService;
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
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = request.FirstName
            };

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
            var employee = new Employee { FirstName = employeeRequest.FirstName };

            await _employeeService.CreateEmployeeAsync(employee);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Employees.Get.Replace("{employeeId}", employee.Id.ToString());

            var response = new EmployeeResponse { FirstName = employee.FirstName };

            return Created(locationUri, response);
        }
    }
}
