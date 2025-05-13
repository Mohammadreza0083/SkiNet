using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public abstract class SpecificationEvaluator<T> 
where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.Criteria != null) 
            query = query.Where(specification.Criteria);
        if (specification.OrderBy != null)
            query = query.OrderBy(specification.OrderBy);
        if (specification.OrderByDesc != null)
            query = query.OrderByDescending(specification.OrderByDesc);
        return query;
    }
}