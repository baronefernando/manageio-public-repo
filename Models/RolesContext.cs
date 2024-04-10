using Microsoft.EntityFrameworkCore;

namespace ManageIO.Models
{
    public class RolesContext : DbContext
    {
        public DbSet<Roles> roles { get; set; }
        public RolesContext(DbContextOptions<RolesContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().Property(b => b.RoleID);
            modelBuilder.Entity<Roles>().HasKey(b => b.RoleName);
        }
        
           
    }
}
