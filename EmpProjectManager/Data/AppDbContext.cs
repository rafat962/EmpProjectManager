using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpProjectManager.models;
using Microsoft.EntityFrameworkCore;

namespace EmpProjectManager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; } 
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\EmpProjectManager.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
