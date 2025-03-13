using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/customers")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IRepository _repository;

    public CustomersController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers([FromQuery] string search = "")
    {
        IEnumerable<CustomersDto> customers = await _repository.GetNextOrderPredictionsAsync();

        if (!string.IsNullOrWhiteSpace(search))
        {
            customers = customers.Where(x => x.CustomerName.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        return Ok(customers);
    }
}
