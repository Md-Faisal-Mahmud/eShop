using Core.Entities;

namespace Core.Specifications;

public class ProductsWithTypesAndBrandSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandSpecification()
    {
        AddInclude(x => x.ProductBrand!);
        AddInclude(x => x.ProductType!);
    }

    public ProductsWithTypesAndBrandSpecification(int id) : base(criteria:x => x.Id == id)
    {
        AddInclude(x => x.ProductBrand!);
        AddInclude(x => x.ProductType!);
    }
}