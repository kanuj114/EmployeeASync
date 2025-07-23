using EmployeeSync.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSync.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) // Constructor Chaining here
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .Property(e => e.Salary) // Configures the 'Salary' property to use decimal(18,4) in SQL Server.
            .HasPrecision(18, 4); // To avoiding precision loss
        }
        public DbSet<Employee> Employees { get; set; } // Employee class converted into Table name after Migrations
    }
}