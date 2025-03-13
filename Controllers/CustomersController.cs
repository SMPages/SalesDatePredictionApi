using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/customers")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IRepository _repository;
    private readonly ILogger<CustomersController> _logger;  

    /// <summary>
    /// Constructor
    /// </summary>
    public CustomersController(IRepository repository, ILogger<CustomersController> logger)
    {
        _repository = repository;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Obtiene una lista de clientes con la opción de búsqueda por nombre.
    /// </summary>
    /// <param name="search">Texto opcional para filtrar clientes por nombre.</param>
    /// <returns>
    /// Retorna una lista de clientes
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomersDto>>> GetCustomers([FromQuery] string search = "")
    {
        try
        {
            var customers = await _repository.GetNextOrderPredictionsAsync() ?? new List<CustomersDto>();

            // Asegurar que no sea nulo antes de usar LINQ
            var customersList = customers.ToList();

            if (!string.IsNullOrWhiteSpace(search))
            {
                customersList = customersList
                    .Where(x => !string.IsNullOrEmpty(x.CustomerName) && x.CustomerName.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!customersList.Any())
            {
                _logger.LogInformation("No se encontraron clientes con el criterio de búsqueda: {Search}", search);
                return NoContent();
            }

            return Ok(customersList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la lista de clientes.");
            return StatusCode(500, "Ocurrió un error interno en el servidor.");
        }
    }
}
