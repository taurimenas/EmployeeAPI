using EmployeeAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees;

        public EmployeeService()
        {
            _employees = new List<Employee>();
            for (int i = 0; i < 5; i++)
            {
                _employees.Add(new Employee { FirstName = new Random().NextDouble().ToString() });
            }
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _employees.SingleOrDefault(x => x.Id == employeeId);
        }

        public List<Employee> GetEmployees()
        {
            return _employees;
        }
    }
}
