using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

public class Repository : IRepository
{
    private readonly string _connectionString;

    public Repository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<CustomersDto>> GetNextOrderPredictionsAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<CustomersDto>(
            "sp_GetNextOrderPrediction",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<ClientOrderDto>> GetClientOrdersAsync(int customerId)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = "EXEC sp_GetClientOrders @CustomerId";
        return await connection.QueryAsync<ClientOrderDto>(sql, new { CustomerId = customerId });
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = "EXEC sp_GetEmployees";
        return await connection.QueryAsync<EmployeeDto>(sql);
    }

    public async Task<IEnumerable<ShipperDto>> GetShippersAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = "EXEC sp_GetShippers";
        return await connection.QueryAsync<ShipperDto>(sql);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<ProductDto>(
            "sp_GetProducts",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<bool> InsertOrderWithProduct(OrderRequest orderRequest)
    {
        using var connection = new SqlConnection(_connectionString);
        var parameters = new
        {
            orderRequest.Empid,
            orderRequest.Shipperid,
            orderRequest.Shipname,
            orderRequest.Shipaddress,
            orderRequest.Shipcity,
            orderRequest.Orderdate,
            orderRequest.Requireddate,
            orderRequest.Shippeddate,
            orderRequest.Freight,
            orderRequest.Shipcountry,
            orderRequest.Productid,
            orderRequest.Unitprice,
            orderRequest.Qty,
            orderRequest.Discount
        };

        try
        {
            await connection.ExecuteAsync(
                "sp_InsertOrderWithProduct",
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return true;
        }
        catch (SqlException)
        {
            return false;
        }
    }
}
