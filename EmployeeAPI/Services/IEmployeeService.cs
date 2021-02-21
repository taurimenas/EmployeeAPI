using EmployeeAPI.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<List<Employee>> GetEmployeesByBossIdAsync(int bossId);
        Task<List<Employee>> GetEmployeesByNameAndBirthIntervalAsync(string firstName, DateTime intervalStart, DateTime intervalEnd);
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<(int count, double averageSalary)> GetCountAndAverageSalaryAsync(string role);
        Task<bool> CreateEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employeeToUpdate);
        Task<bool> UpdateEmployeeSalaryAsync(Employee employee, double salary);
        Task<bool> DeleteEmployeeAsync(int employeeId);
    }
}
