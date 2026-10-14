using EF_Core.Entities;

namespace EF_Core.Data
{
    public class ProductsSource
    {
        public static readonly List<Product> Products = new()
    {
        new Product
        {
            Id = 1, Name = "Laptop", Price = 25000, CategoryId = 1,
            Category = new Category { Id = 1, Name = "Electronics" }
        },
        new Product
        {
            Id = 2, Name = "shirt", Price = 4000, CategoryId = 2,
            Category = new Category { Id = 2, Name = "Clothing" }
        },
        new Product
        {
            Id = 3, Name = "Jeans", Price = 4000, CategoryId = 2,
            Category = new Category { Id = 2, Name = "Clothing" }
        }
    };
    }
}
