using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IRepository _repository;

    public ProductsController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _repository.GetProductsAsync();
        return Ok(products);
    }
}
