using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Domain
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public Employee Boss { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
        public string Role { get; set; } // Should be enum
    }
}
