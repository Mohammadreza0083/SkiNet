using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public abstract class SpecificationEvaluator<T> 
where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.Criteria is not null) 
            query = query.Where(specification.Criteria);
        if (specification.OrderBy is not null)
            query = query.OrderBy(specification.OrderBy);
        if (specification.OrderByDesc is not null)
            query = query.OrderByDescending(specification.OrderByDesc);
        if (specification.IsDistinct)
            query = query.Distinct();
        if (specification.IsPagingEnable)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }
        return query;
    }
    public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query, 
        ISpecification<T, TResult> specification)
    {
        if (specification.Criteria is not null) 
            query = query.Where(specification.Criteria);
        if (specification.OrderBy is not null)
            query = query.OrderBy(specification.OrderBy);
        if (specification.OrderByDesc is not null)
            query = query.OrderByDescending(specification.OrderByDesc);
        var selectQuery = query as IQueryable<TResult>;
        if (specification.Select is not null)
            selectQuery = query.Select(specification.Select);
        if (specification.IsDistinct && selectQuery is not null)
            selectQuery = selectQuery.Distinct();
        if (specification.IsPagingEnable && selectQuery is not null)
        {
            selectQuery = selectQuery.Skip(specification.Skip).Take(specification.Take);
        }
        return selectQuery ?? query.Cast<TResult>();
    }
}