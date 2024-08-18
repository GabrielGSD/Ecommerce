namespace Ecommerce.Application.Interfaces;

public interface ICustomerApplicationService
{
    void SaveCustomer(CustomerDto customer);
    void UpdateCustomer(CustomerDto customer);
    void DeleteCustomer(string id);
    IEnumerable<CustomerDto> GetCustomers();
    CustomerDto GetCustomerById(string id);
}