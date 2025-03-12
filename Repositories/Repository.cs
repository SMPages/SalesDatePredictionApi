using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

public class Repository : IRepository
{
    private readonly IDbConnection _dbConnection;

    public Repository(IConfiguration configuration)
    {
        _dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<IEnumerable<CustomersDto>> GetNextOrderPredictionsAsync()
    {
        var result = await _dbConnection.QueryAsync<CustomersDto>(
            "sp_GetNextOrderPrediction", 
            commandType: CommandType.StoredProcedure
        );
        return result;
    }

    public async Task<IEnumerable<ClientOrderDto>> GetClientOrdersAsync(int customerId)
    {
        string sql = "EXEC sp_GetClientOrders @CustomerId";
        return await _dbConnection.QueryAsync<ClientOrderDto>(sql, new { CustomerId = customerId });
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
    {
        string sql = "EXEC sp_GetEmployees";
        return await _dbConnection.QueryAsync<EmployeeDto>(sql);
    }

    public async Task<IEnumerable<ShipperDto>> GetShippersAsync()
    {
        string sql = "EXEC sp_GetShippers";
        return await _dbConnection.QueryAsync<ShipperDto>(sql);
    }
    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        return await _dbConnection.QueryAsync<ProductDto>("sp_GetProducts", commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> InsertOrderWithProduct(OrderRequest orderRequest)
    {
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
            await _dbConnection.ExecuteAsync("sp_InsertOrderWithProduct", parameters, commandType: CommandType.StoredProcedure);
            return true;
        }
        catch (SqlException)
        {
            return false;
        }
    }
}