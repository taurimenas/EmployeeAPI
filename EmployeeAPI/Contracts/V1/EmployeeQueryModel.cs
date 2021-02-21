using System;

namespace EmployeeAPI.Contracts.V1
{
    public class EmployeeQueryModel
    {
        public string FirstName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? BossId { get; set; }
    }
}
