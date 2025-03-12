public interface IRepository
{
    Task<IEnumerable<CustomersDto>> GetNextOrderPredictionsAsync();
    Task<IEnumerable<ClientOrderDto>> GetClientOrdersAsync(int customerId);
    Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
    Task<IEnumerable<ShipperDto>> GetShippersAsync();
    Task<IEnumerable<ProductDto>> GetProductsAsync();

    Task<bool> InsertOrderWithProduct(OrderRequest orderRequest);
}