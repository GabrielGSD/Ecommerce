namespace Ecommerce.Infrastructure.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDocumentStore _documentStore;

    public CustomerRepository(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }
    
    public void Insert(Customer customer)
    {
        using IDocumentSession documentSession = _documentStore.OpenSession();
        documentSession.Store(customer);
        documentSession.SaveChanges();
    }

    public void Update(Customer customer)
    {
        using IDocumentSession documentSession = _documentStore.OpenSession();
        var customerExist = documentSession.Query<Customer>()
                                            .FirstOrDefault(c => c.Name == customer.Name);
        if (customerExist == null)
        {
            throw new Exception("Customer not found");
        }
        customerExist.Name = customer.Name;
        customerExist.LastName = customer.LastName;
        customerExist.Email = customer.Email;
        customerExist.BirthDate = customer.BirthDate;
        customerExist.Address = customer.Address;
        customerExist.Cpf = customer.Cpf;
        customerExist.IsActive = customer.IsActive;
        
        documentSession.SaveChanges();
    }

    public void Delete(string id)
    {
        using IDocumentSession documentSession = _documentStore.OpenSession();
        var customerExist = documentSession.Load<Customer>(id);
        if (customerExist == null)
        {
            throw new Exception("Customer not found");
        }
        documentSession.Delete(customerExist);
        documentSession.SaveChanges();
    }

    public IEnumerable<Customer> Get()
    {
        using IDocumentSession documentSession = _documentStore.OpenSession();
        return documentSession.Query<Customer>().ToList();
    }

    public Customer Get(string id)
    {
        using IDocumentSession documentSession = _documentStore.OpenSession();
        return documentSession.Load<Customer>(id);
    }

    public Customer? GetByEmail(string email)
    {
        using IDocumentSession documentSession = _documentStore.OpenSession();
        var customerExist = documentSession.Query<Customer>()
            .FirstOrDefault(c => c.Email == email);
        return customerExist;
    }
}