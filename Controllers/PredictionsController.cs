using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PredictionsController : ControllerBase
{
    private readonly IRepository _repository;

    public PredictionsController(IRepository repository)
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