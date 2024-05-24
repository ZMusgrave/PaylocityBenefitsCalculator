using Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Api.Data;

public class CompanyContext : DbContext
{
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(c => c.Dependents)
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeId);

            modelBuilder.Seed();
        }

        public DbSet<Dependent> Dependents { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
}
