namespace Ecommerce.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public void SaveCustomer(Customer customer)
    {
        ValidateEmail(customer.Email);
        customer.IsActive = true;
        customer.CreatedDate = DateTime.Now;
        customer.Address.IsActive = true;
        _customerRepository.Insert(customer);
    }

    public void UpdateCustomer(Customer customer)
    {
        _customerRepository.Update(customer);
    }

    public void DeleteCustomer(string id)
    {
        _customerRepository.Delete(id);
    }

    public IEnumerable<Customer> GetCustomers()
    {
        return _customerRepository.Get();
    }

    public Customer GetCustomerById(string id)
    {
        return _customerRepository.Get(id);
    }

    private void ValidateEmail(string email)
    {
        isEmailValid(email);
        
        var existingCustomer = _customerRepository.GetByEmail(email);
        if (existingCustomer != null)
            throw new DuplicateEmailException(email);
    }
    
    private bool isEmailValid(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException("Email cannot be null or empty.");
        try
        {
            var emailAddress = new MailAddress(email);
            if(emailAddress.Address != email)
                return false;
            return CheckDomainHasMXRecord(emailAddress.Host);
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }
    
    private bool CheckDomainHasMXRecord(string domain)
    {
        try
        {
            var lookup = new LookupClient();
            var result = lookup.Query(domain, QueryType.MX);
            return result.Answers.MxRecords().Any();
        }
        catch (Exception e)
        {
            return false;
        }
    }
}