using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, [NotNull] Expression<Func<TSource, bool>> predicate)
                 => condition ? source.Where(predicate) : source;
    }
}
