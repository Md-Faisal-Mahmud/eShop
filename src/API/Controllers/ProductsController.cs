using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IBaseRepository<Product> _productRepo;
    private readonly IBaseRepository<ProductBrand> _productBrandRepo;
    private readonly IBaseRepository<ProductType> _productTypeRepo;
    private readonly IMapper _mapper;
    

    public ProductsController(IBaseRepository<Product> productRepo, 
        IBaseRepository<ProductBrand> productBrandRepo, 
        IBaseRepository<ProductType> productTypeRepo, IMapper mapper)
    {
        _productRepo = productRepo;
        _productBrandRepo = productBrandRepo;
        _productTypeRepo = productTypeRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    {
        var spec = new ProductsWithTypesAndBrandSpecification(productParams);
        var countSpec = new ProductWithFiltersForCountSpecification(productParams);
        var totalItems = await _productRepo.CountAsync(countSpec);
        
        var productList = await _productRepo.ListAsync(spec);
        var productListToReturn = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(productList);

        return Ok(new Pagination<ProductToReturnDto>(
            productParams.PageIndex, 
            productParams.PageSize, 
            totalItems, productListToReturn));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandSpecification(id);
        Product product = await _productRepo.GetEntityWithSpec(spec);

        if (product is null) return NotFound(new ApiResponse(404));

        var productToReturn = _mapper.Map<Product, ProductToReturnDto>(product);
        
        return Ok(productToReturn);
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