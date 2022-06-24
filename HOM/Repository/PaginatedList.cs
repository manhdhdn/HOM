using HOM.Models;
using Microsoft.EntityFrameworkCore;

namespace HOM.Repository
{
    public class PaginatedList<T>
    {
        public static async Task<PagedModel<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }

            if (pageSize == 0)
            {
                pageSize = 8;
            }
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedModel<T>(items, count, pageIndex, pageSize);
        }
    }
}
