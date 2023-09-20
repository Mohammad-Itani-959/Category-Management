namespace BulkyBook.Api.Model
{
    public class RecordsViewList
    {
        public IEnumerable<Category> Records { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
