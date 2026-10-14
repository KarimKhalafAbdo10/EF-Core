using EF_Core.Data;
using EF_Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {

            var query = _context.Products
               .AsNoTracking() 
               .Select(p => new Product 
               {
                   Id = p.Id,
                   Name = p.Name,
                   Price = p.Price,
                   Category = p.Category 
               });

            var totalCount = await query.CountAsync();

            var products = await query
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync(); 

            return Ok(new { totalCount, products });
        }

        public record UpdateProductDto(string Name, decimal Price, byte[] RowVersion);

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            product.Name = dto.Name;
            product.Price = dto.Price;

            _context.Entry(product).Property(p => p.RowVersion).OriginalValue = dto.RowVersion;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Data was modified by another user. Please reload." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.IsDeleted = true; // مش _context.Remove
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
