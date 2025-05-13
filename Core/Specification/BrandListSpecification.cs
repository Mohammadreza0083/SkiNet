using Core.Entities;

namespace Core.Specification;

public class BrandListSpecification : BaseSpecification<Product, string>
{
    public BrandListSpecification()
    {
        SetSelect(x => x.Brand);
        ApplyDistinct();
    }
}