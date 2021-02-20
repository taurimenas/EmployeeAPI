using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Entities
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=employee.db"); //TODO: Hide connection string
    }
}
