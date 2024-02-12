using Microsoft.EntityFrameworkCore;
using ShoppingApp.Models;

namespace ShoppingApp.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Categories>tblCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>().HasData(
                new Categories { CategoryId=1,CategoryName="Action",DisplayOrder=1},
                new Categories { CategoryId=2,CategoryName="SciFi",DisplayOrder=2},
                new Categories { CategoryId=3,CategoryName="History",DisplayOrder=3}
                );
        }
    }
}
