using EmployeeAPI.Contracts.V1.Requests;
using EmployeeAPI.Contracts.V1.Responses;
using EmployeeAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Mappings.V1
{
    public class EmployeeMappings
    {
        public static Employee ToEmployee(CreateEmployeeRequest employeeRequest, List<Employee> employees)
        {
            return new Employee
            {
                FirstName = employeeRequest.FirstName,
                LastName = employeeRequest.LastName,
                BirthDate = employeeRequest.BirthDate,
                EmploymentDate = employeeRequest.EmploymentDate,
                Address = employeeRequest.Address,
                Salary = employeeRequest.Salary,
                Role = employeeRequest.Role,
                Boss =  employees.FirstOrDefault(x => x.Id == employeeRequest.BossId) 
            };
        }

        public static Employee ToEmployee(Employee employee, UpdateEmployeeRequest employeeRequest, List<Employee> employees)
        {
            employee.FirstName = employeeRequest.FirstName;
            employee.LastName = employeeRequest.LastName;
            employee.BirthDate = employeeRequest.BirthDate;
            employee.EmploymentDate = employeeRequest.EmploymentDate;
            employee.Address = employeeRequest.Address;
            employee.Salary = employeeRequest.Salary;
            employee.Role = employeeRequest.Role;
            employee.Boss = employees.FirstOrDefault(x => x.Id == employeeRequest.BossId);
            return employee;
        }

        public static EmployeeResponse ToEmployeeResponse(Employee employee)
        {
            return new EmployeeResponse
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                EmploymentDate = employee.EmploymentDate,
                Boss = employee.Boss,
                Address = employee.Address,
                Salary = employee.Salary,
                Role = employee.Role
            };
        }

        public static CreateEmployeeRequest ToCreateEmployeeRequest(Employee employee)
        {
            return new CreateEmployeeRequest
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                EmploymentDate = employee.EmploymentDate,
                Address = employee.Address,
                Salary = employee.Salary,
                Role = employee.Role,
                BossId = employee.Boss.Id
            };
        }

        public static UpdateEmployeeRequest ToUpdateEmployeeRequest(Employee employee)
        {
            return new UpdateEmployeeRequest
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                EmploymentDate = employee.EmploymentDate,
                Address = employee.Address,
                Salary = employee.Salary,
                Role = employee.Role,
                BossId = employee.Boss.Id
            };
        }
    }
}
