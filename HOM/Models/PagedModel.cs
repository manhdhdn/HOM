namespace HOM.Models
{
    public class PagedModel<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecord { get; set; }
        public List<T> Data { get; set; } = new();

        public PagedModel(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalRecord = count;

            Data.AddRange(items);
        }

    }
}
