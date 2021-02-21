using EmployeeAPI.Data;
using EmployeeAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace EmployeeAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _dataContext;

        public EmployeeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _dataContext.Employees.ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesByBossIdAsync(int bossId)
        {
            var employees = await GetEmployeesAsync();
            return employees.Where(x => x.Boss != null && x.Boss.Id == bossId).ToList();
        }

        public async Task<List<Employee>> GetEmployeesByNameAndBirthIntervalAsync(string firstName, DateTime intervalStart, DateTime intervalEnd)
        {
            var employees = await GetEmployeesAsync();
            return employees.Where(x => x.FirstName == firstName && x.BirthDate >= intervalStart && x.BirthDate <= intervalEnd).ToList();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _dataContext.Employees.SingleOrDefaultAsync(x => x.Id == employeeId); //Throws error if more than one id is the same
        }

        public async Task<(int count, double averageSalary)> GetCountAndAverageSalaryAsync(string role)
        {
            var employees = await GetEmployeesAsync();
            var employessFilter = employees.Where(x => x.Role == role);
            var count = employees.Where(x => x.Role == role).Count();
            var averageSalary = employessFilter.Sum(x => x.Salary) / count;
            return (count, averageSalary);
        }

        public async Task<bool> CreateEmployeeAsync(Employee employee)
        {
            await _dataContext.Employees.AddAsync(employee);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateEmployeeSalaryAsync(Employee employee, double salary)
        {
            employee.Salary = salary;
            _dataContext.Employees.Update(employee);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employeeToUpdate)
        {
            _dataContext.Employees.Update(employeeToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            var employee = await GetEmployeeByIdAsync(employeeId);
            _dataContext.Employees.Remove(employee);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
