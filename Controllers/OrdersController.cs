using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IRepository _repository;

    public OrdersController(IRepository repository)
    {
        _repository = repository;
    }

     [HttpGet("{customerId}")]
    public async Task<IActionResult> GetClientOrders(int customerId)
    {
        var orders = await _repository.GetClientOrdersAsync(customerId);
        return Ok(orders);
    }

    [HttpPost("create")]
    public async Task<IActionResult> InsertOrderWithProduct([FromBody] OrderRequest orderRequest)
    {
        if (orderRequest == null)
            return BadRequest("Invalid request");

        bool result = await _repository.InsertOrderWithProduct(orderRequest);

        if (result)
            return Ok(new { message = "Order created successfully" });

        return StatusCode(500, new { error = "Error inserting order" });
    }
}