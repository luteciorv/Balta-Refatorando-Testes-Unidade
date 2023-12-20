using Store.Domain.Entities;
using System.Linq.Expressions;

namespace Store.Domain.Queries;

public static class ProductQueries
{
    public static Expression<Func<Product, bool>> GetActiveProducts() =>
        p => p.Active;

    public static Expression<Func<Product, bool>> GetInactiveProducts() =>
        p => !p.Active;
}
