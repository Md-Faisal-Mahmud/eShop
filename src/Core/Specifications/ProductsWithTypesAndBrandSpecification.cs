﻿using Core.Entities;

namespace Core.Specifications;

public class ProductsWithTypesAndBrandSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandSpecification(string sort)
    {
        AddInclude(x => x.ProductBrand!);
        AddInclude(x => x.ProductType!);
        AddOrderBy(x => x.Name!);

        if (!string.IsNullOrEmpty(sort))
        {
            switch (sort)
            {
                case "priceAsc": AddOrderBy(p => p.Price); break;
                case "priceDesc": AddOrderByDescending(p => p.Price); break;
                default: AddOrderBy(n => n.Name!);break;

            }
        }
    }

    public ProductsWithTypesAndBrandSpecification(int id) : base(criteria:x => x.Id == id)
    {
        AddInclude(x => x.ProductBrand!);
        AddInclude(x => x.ProductType!);
    }
}