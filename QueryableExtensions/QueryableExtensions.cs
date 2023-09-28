using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;

namespace ProvaPub.QueryableExtensions;

public static class QueryableExtensions
{
    private const int PageSize = 10;

    public static async Task<PagedList<T>> GetPagedList<T>(this IQueryable<T> query, int page) where T : class
    {
        if (page < 1) page = 1;

        var totalCount = query.Count();
        var items = await query.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

        return new PagedList<T>
        {
            HasNext = (page * PageSize) < totalCount,
            TotalCount = totalCount,
            Items = items
        };
    }
}
