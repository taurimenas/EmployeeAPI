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
        public IActionResult GetAll()
        {
            return Ok(_employeeService.GetEmployees());
        }

        [HttpGet(ApiRoutes.Employees.Get)]
        public IActionResult Get([FromRoute] int employeeId )
        {
            var employee = _employeeService.GetEmployeeById(employeeId);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost(ApiRoutes.Employees.Create)]
        public IActionResult Create([FromBody] CreateEmployeeRequest employeeRequest)
        {
            var employee = new Employee { FirstName = employeeRequest.FirstName };

            _employeeService.GetEmployees().Add(employee);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Employees.Get.Replace("{employeeId}", employee.Id.ToString());

            var response = new EmployeeResponse { FirstName = employee.FirstName };

            return Created(locationUri, response);
        }
    }
}
