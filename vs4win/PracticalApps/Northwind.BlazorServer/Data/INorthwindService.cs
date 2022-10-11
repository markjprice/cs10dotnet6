namespace Packt.Shared;

public interface INorthwindService
{
  Task<List<Customer>> GetCustomersAsync();
  List<string?> GetCountries();
  Task<List<Customer>> GetCustomersAsync(string country);
  Task<Customer?> GetCustomerAsync(string id);
  Task<Customer> CreateCustomerAsync(Customer c);
  Task<Customer> UpdateCustomerAsync(Customer c);
  Task DeleteCustomerAsync(string id);
}
