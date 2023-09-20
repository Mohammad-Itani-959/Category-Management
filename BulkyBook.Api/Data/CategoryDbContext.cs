using BulkyBook.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Api.Data
{
    public class CategoryDbContext: DbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options)
        {


        }

        public DbSet<Category> Categories { get; set; }

    }
}
