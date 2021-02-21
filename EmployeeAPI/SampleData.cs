using EmployeeAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI
{
    public class SampleData
    {
        public static List<Employee> Load()
        {
            return AddBosses(CreateEmployees());
        }

        private static List<Employee> AddBosses(List<Employee> employees)
        {
            employees[1].Boss = employees[0];
            employees[2].Boss = employees[1];
            employees[3].Boss = employees[1];
            employees[4].Boss = employees[0];
            employees[5].Boss = employees[0];
            return employees;
        }

        private static List<Employee> CreateEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    FirstName = "Darcie",
                    LastName = "Jemima",
                    Address = "79  York Road",
                    BirthDate = DateTime.Now.AddYears(-30),
                    EmploymentDate = DateTime.Now.AddYears(-20),
                    Salary = Math.Round(new Random().NextDouble(), 3)  * 100000,
                    Role = "CEO",
                },
                new Employee
                {
                    FirstName = "Kellen",
                    LastName = "Mickey",
                    Address = "80  Caerfai Bay Road",
                    BirthDate = DateTime.Now.AddYears(-30),
                    EmploymentDate = DateTime.Now.AddYears(-10),
                    Salary = Math.Round(new Random().NextDouble(), 3) * 10000,
                    Role = "Project Manager",
                },
                new Employee
                {
                    FirstName = "Lara",
                    LastName = "Dorothy",
                    Address = "146  Bishopgate Street",
                    BirthDate = DateTime.Now.AddYears(-30),
                    EmploymentDate = DateTime.Now.AddYears(-5),
                    Salary = Math.Round(new Random().NextDouble(), 3) * 10000,
                    Role = "Software Developer",
                },
                new Employee
                {
                    FirstName = "Case",
                    LastName = "Abel",
                    Address = "140  Bishopgate Street",
                    BirthDate = DateTime.Now.AddYears(-30),
                    EmploymentDate = DateTime.Now.AddYears(-3),
                    Salary = Math.Round(new Random().NextDouble(), 3) * 10000,
                    Role = "Software Developer",
                },
                new Employee
                {
                    FirstName = "Gordy",
                    LastName = "Biddy",
                    Address = "45  St Dunstans Street",
                    BirthDate = DateTime.Now.AddYears(-30),
                    EmploymentDate = DateTime.Now.AddYears(-4),
                    Salary = Math.Round(new Random().NextDouble(), 3) * 10000,
                    Role = "Engineer",
                },
                new Employee
                {
                    FirstName = "Adrienne",
                    LastName = "Sparrow",
                    Address = "21  Simone Weil Avenue",
                    BirthDate = DateTime.Now.AddYears(-30),
                    EmploymentDate = DateTime.Now.AddYears(-7),
                    Salary = Math.Round(new Random().NextDouble(), 3) * 10000,
                    Role = "Engineer",
                },
            };
        }
    }
}
