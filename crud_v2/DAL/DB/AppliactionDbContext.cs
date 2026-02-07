using crud_v2.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace crud_v1.DAL.DB
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=EMPDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Employee> employees { get; set; }
        
    }
}