using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IBaseRepository<Product> _productRepo;
    private readonly IBaseRepository<ProductBrand> _productBrandRepo;
    private readonly IBaseRepository<ProductType> _productTypeRepo;
    

    public ProductsController(IBaseRepository<Product> productRepo, 
        IBaseRepository<ProductBrand> productBrandRepo, 
        IBaseRepository<ProductType> productTypeRepo)
    {
        _productRepo = productRepo;
        _productBrandRepo = productBrandRepo;
        _productTypeRepo = productTypeRepo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandSpecification();
        
        var productList = await _productRepo.ListAsync(spec);

        return Ok(productList);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandSpecification(id);
        
        Product product = await _productRepo.GetEntityWithSpec(spec);
        return Ok(product);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        var productBrandList = await _productBrandRepo.ListAllAsync();

        return Ok(productBrandList);
    }
    
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        var productTypeList = await _productTypeRepo.ListAllAsync();

        return Ok(productTypeList);
    }
}