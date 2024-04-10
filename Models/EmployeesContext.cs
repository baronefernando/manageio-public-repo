using Microsoft.EntityFrameworkCore;

namespace ManageIO.Models
{
    public class EmployeesContext : DbContext
    {
        public DbSet<Employees> employees { get; set; }
        public EmployeesContext(DbContextOptions<EmployeesContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>().HasKey(b => b.EmployeeID);
            modelBuilder.Entity<Roles>().HasKey(b => b.RoleName);
            modelBuilder.Entity<Employees>().Property(b => b.FullName);
            modelBuilder.Entity<Employees>().Property(b => b.Email);
            modelBuilder.Entity<Employees>().Property(b => b.Address);
            modelBuilder.Entity<Employees>().Property(b => b.Mobile);
            modelBuilder.Entity<Employees>().Property(b => b.Salary);
        }
    }
}
