using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IRepository _repository;

    public EmployeesController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _repository.GetEmployeesAsync();
        return Ok(employees);
    }
}
