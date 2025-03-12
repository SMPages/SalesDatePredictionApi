using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IRepository _repository;

    public CustomersController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("next-order")]
    public async Task<IActionResult> GetNextOrderPrediction()
    {
        var result = await _repository.GetNextOrderPredictionsAsync();
        return Ok(result);
    }
}