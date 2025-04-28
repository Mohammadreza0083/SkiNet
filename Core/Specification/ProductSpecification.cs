using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification;

public class ProductSpecification(string? brand, string? type) : BaseSpecification<Product>(x =>
    (string.IsNullOrEmpty(brand) || x.Brand == brand) &&
    (string.IsNullOrEmpty(type) || x.Type == type));