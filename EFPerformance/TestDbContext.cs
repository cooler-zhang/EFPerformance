using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPerformance
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
            : base("pwd=abc.123;uid=sa;database=TestDb;server=intdevserver.ygsm.com;Trusted_Connection=False;Persist Security Info=True;")
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>()
            //    .HasOptional(s => s.Category)
            //    .WithMany(s => s.Products);
        }
    }
}
