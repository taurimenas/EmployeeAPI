using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.Domain;

namespace EmployeeAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite("Data Source=employee.db"); //TODO: Hide connection string

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
