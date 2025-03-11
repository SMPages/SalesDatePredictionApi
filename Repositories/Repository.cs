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

    public async Task<IEnumerable<NextOrderPredictionDto>> GetNextOrderPredictionsAsync()
    {
        var result = await _dbConnection.QueryAsync<NextOrderPredictionDto>(
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

}