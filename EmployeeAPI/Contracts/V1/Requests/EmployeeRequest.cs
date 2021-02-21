using System;

namespace EmployeeAPI.Contracts.V1.Requests
{
    public class EmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
        public string Role { get; set; }

        public int BossId { get; set; }
    }
}
