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
        return selectQuery ?? query.Cast<TResult>();
    }
}