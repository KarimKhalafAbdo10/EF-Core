using System.ComponentModel.DataAnnotations;

namespace EF_Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }= default!;

        public bool IsDeleted { get; set; } // 5. Soft Delete

        [Timestamp] // 7. Concurrency Token
        public byte[] RowVersion { get; set; }= default!;
    }

   
}
