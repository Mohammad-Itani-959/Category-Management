using BulkyBook.Api.Data;
using Microsoft.EntityFrameworkCore;
using BulkyBook.Api.Model;

namespace BulkyBook.Api.Service
{
    public class CategoryService: ICategoryService
    {
        private readonly CategoryDbContext _dbContext;

        public CategoryService(CategoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<RecordsViewList> GetCategories(int pageNumber)
        {
            int pageSize = 5;
            IEnumerable<Category> categories = _dbContext.Categories.Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
            int total = _dbContext.Categories.Count();

            int totalPages = (int)Math.Ceiling((double)total / pageSize);

            var data = new RecordsViewList
            {
                Records = categories,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages

            };

            return data;
        }

        public async Task Add(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryId(int id)
        {
            var cat = await _dbContext.Categories.FindAsync(id);
            return cat;
        }

        public async Task Update(Category category)
        {
            try
            {
                var cat = await _dbContext.Categories.FirstAsync(c=> c.Id ==category.Id);
                if(cat != null)
                {
                    cat.Name = category.Name;
                    cat.DisplayedOrder = category.DisplayedOrder;
                    

                }
                await _dbContext.SaveChangesAsync();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                
            }
            

        }
        public async Task Delete(Category category)
        {
            var cat = await _dbContext.Categories.FindAsync(category.Id);


            _dbContext.Categories.Remove(cat);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Count()
        {
            return await _dbContext.Categories.CountAsync();
        }
    }
}
