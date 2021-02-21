using EmployeeAPI.Contracts.V1;
using EmployeeAPI.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeesAsync(EmployeeQueryModel employeeQueryModel = null);
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<(int count, double averageSalary)> GetCountAndAverageSalaryAsync(string role);
        Task<bool> CreateEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employeeToUpdate);
        Task<bool> UpdateEmployeeSalaryAsync(Employee employee, double salary);
        Task<bool> DeleteEmployeeAsync(int employeeId);
    }
}
