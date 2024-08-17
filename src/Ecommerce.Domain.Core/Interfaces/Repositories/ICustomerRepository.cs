namespace Ecommerce.Domain.Core.Interfaces.Repositories;

public interface ICustomerRepository
{
    void Insert(Customer customer);
    void Update(Customer customer);
    void Delete(string id);
    IEnumerable<Customer> Get();
    Customer Get(string id);
    Customer? GetByEmail(string email);
}