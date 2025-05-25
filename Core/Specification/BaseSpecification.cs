using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specification;

public class BaseSpecification <T> (Expression<Func<T,bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification() : this(null){}
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDesc {get; private set;}

    public bool IsDistinct { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnable { get; private set; }
    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if (criteria is not null)
        {
            query = query.Where(criteria);
        }

        return query;
    }

    protected void SetOrderBy(Expression<Func<T, object>>? orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    
    protected void SetOrderByDesc(Expression<Func<T, object>>? orderByDescExpression)
    {
        OrderByDesc = orderByDescExpression;
    }
    
    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnable = true;
    }
}
public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria)
    : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecification() : this(null){}
    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void SetSelect(Expression<Func<T, TResult>>? selectExpression)
    {
        Select = selectExpression;
    }
}