using System;
using System.Linq;
using System.Linq.Expressions;

namespace CodeCube.Core.Extensions
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Distinct the collection in the IQueryable on a desired property.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <see cref="https://stackoverflow.com/a/40573585/291293"/>
        /// <returns></returns>
        public static IQueryable<TSource> DistinctBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            return source.GroupBy(keySelector).Select(x => x.FirstOrDefault());
        }
    }
}