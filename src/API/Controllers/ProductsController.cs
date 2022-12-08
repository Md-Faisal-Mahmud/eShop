using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // GET
    [HttpGet]
    public string GetProducts()
    {
        return "This will be a list of products";
    }

    [HttpGet("{id:int}")]
    public string GetProduct(int id)
    {
        return "Single product";
    }
}