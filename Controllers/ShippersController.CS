using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/shippers")]
[ApiController]
public class ShippersController : ControllerBase
{
    private readonly IRepository _repository;

    public ShippersController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetShippers()
    {
        var shippers = await _repository.GetShippersAsync();
        return Ok(shippers);
    }
}
