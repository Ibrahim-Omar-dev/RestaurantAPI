namespace Restaurants.Application.Common
{
    public class PageResult<T>
    {

        public IEnumerable<T> Items { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPages { get; set; }
        public int FromItem { get; set; }
        public int ToItem { get; set; }
        public PageResult(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            FromItem = pageSize * (pageNumber - 1) + 1;
            ToItem = FromItem + pageSize - 1;
        }
    }
}
