using Domain.Models.Pagination;

namespace Common.Extensions
{
    public static class QueryableExtensions
    {
        // Pagination
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationRequestModel pagination)
        {
            if (pagination.PageNumber < 0)
            {
                pagination.PageNumber = 0;
            }
            if (pagination.PageSize < 1)
            {
                pagination.PageSize = 10;
            }
            return query.Skip((pagination.PageNumber) * pagination.PageSize).Take(pagination.PageSize);
        }

    }
}
