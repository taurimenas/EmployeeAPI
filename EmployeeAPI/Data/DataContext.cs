using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Domain;

namespace EmployeeAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
