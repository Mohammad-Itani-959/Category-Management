using BulkyBook.Api.Model;
namespace BulkyBook.Api.Service

{
    public interface ICategoryService //Interface
    {
        Task<RecordsViewList> GetCategories(int pageNumber);

        Task Add(Category category);
        Task<Category> GetCategoryId(int id);

        Task Update(Category category);
        Task Delete(Category category);

        Task<int> Count();
    }
}
