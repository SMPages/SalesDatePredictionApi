public interface IRepository
{
    Task<IEnumerable<NextOrderPredictionDto>> GetNextOrderPredictionsAsync();
    Task<IEnumerable<ClientOrderDto>> GetClientOrdersAsync(int customerId);
    Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
    Task<IEnumerable<ShipperDto>> GetShippersAsync();
}