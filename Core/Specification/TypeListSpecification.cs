using Core.Entities;

namespace Core.Specification;

public class TypeListSpecification : BaseSpecification<Product, string>
{
    public TypeListSpecification()
    {
        SetSelect(x => x.Type);
        ApplyDistinct();
    }
}