using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Contracts.V1.Requests
{
    public class AverageSalaryRequest
    {
        public int Count { get; set; }
        public double AverageSalary { get; set; }
    }
}
