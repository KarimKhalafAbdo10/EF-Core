using EF_Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_Core.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // 5. Global Filter - Soft Delete
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);

            // 4. Index - اختار Name لانه اكثر حقل يتعمل عليه Search
            // السبب: البحث بالاسم LIKE '%...%' سيكون اسرع بكتير
            modelBuilder.Entity<Product>().HasIndex(p => p.Name);

            // Index تاني مهم لـ CategoryId عشان الـ Join
            modelBuilder.Entity<Product>().HasIndex(p => p.CategoryId);
            modelBuilder.Entity<Product>()
    .Property(p => p.RowVersion)
    .IsRowVersion();

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Furniture" }
            );


        }
    }
}
