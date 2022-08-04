using Dashome.Core.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace Dashome.Core.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string orderBy, bool orderDesc = true)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return query;

        PropertyInfo? property = typeof(T).GetProperty(orderBy.ToPascalCase());

        if (property == null) return query;
        
        ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");
        MemberExpression memberExpression = Expression.Property(parameterExpression, property.Name);
        Expression conversionExpression = Expression.Convert(memberExpression, typeof(object));
        Expression<Func<T, object>> lambdaExpression =
            Expression.Lambda<Func<T, object>>(conversionExpression, parameterExpression);
        query = orderDesc ? query.OrderByDescending(lambdaExpression) : query.OrderBy(lambdaExpression);

        return query;
    }

    public static IQueryable<T> AddQueryParameters<T>(this IQueryable<T> query,
        QueryParameters? queryParameters)
    {
        if (queryParameters == null)
            return query;
        if (!string.IsNullOrWhiteSpace(queryParameters.OrderBy))
            query = query.OrderBy(queryParameters.OrderBy, queryParameters.OrderDesc ?? true);

        if (queryParameters.Skip != null) query = query.Skip(queryParameters.Skip.Value);
        if (queryParameters.Limit != null) query = query.Take(queryParameters.Limit.Value);

        return query;
    }

    public static PagedResult<TResult> ToPagedResult<TResult>(this IQueryable<TResult> query,
        QueryParameters? parameters)
    {
        var result = new PagedResult<TResult>
        {
            TotalItems = query.Count(),
            Result = parameters != null ? query.AddQueryParameters(parameters).ToList() : query.ToList()
        };

        return result;
    }
}