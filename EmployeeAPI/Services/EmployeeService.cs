using EmployeeAPI.Data;
using EmployeeAPI.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Contracts.V1;

namespace EmployeeAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _dataContext;

        public EmployeeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Employee>> GetEmployeesAsync(EmployeeQueryModel employeeQueryModel = null)
        {
            IQueryable<Employee> employees = _dataContext.Employees
                .Include(x => x.Boss);

            if (!(employeeQueryModel is null))
            {
                if (employeeQueryModel.BossId.HasValue)
                {
                    employees = employees.Where(x => x.Boss.Id == employeeQueryModel.BossId.Value);
                }
                if (!string.IsNullOrEmpty(employeeQueryModel.FirstName))
                {
                    employees = employees.Where(x => x.FirstName.ToLower() == employeeQueryModel.FirstName.ToLower());
                }
                if (employeeQueryModel.DateFrom.HasValue)
                {
                    employees = employees.Where(x => x.BirthDate >= employeeQueryModel.DateFrom.Value);
                }
                if (employeeQueryModel.DateTo.HasValue)
                {
                    employees = employees.Where(x => x.BirthDate <= employeeQueryModel.DateTo.Value);
                }
            }
            return await employees.ToListAsync();
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
