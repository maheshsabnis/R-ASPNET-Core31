using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp31.Models
{
    public class EAppSoppingContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public EAppSoppingContext(DbContextOptions<EAppSoppingContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // one-to-many relationship from Category to Product
            modelBuilder.Entity<Product>()
                 .HasOne(p => p.Category) // one-to-one
                 .WithMany(c => c.Products) // one-to-many
                 .HasForeignKey(p => p.CategoryRowId);
        }
    }
}
