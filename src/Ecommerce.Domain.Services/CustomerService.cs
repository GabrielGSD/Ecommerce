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
        _customerRepository.Insert(customer);
    }
    
    private void ValidateEmail(string email)
    {
        if (!isEmailValid(email))
            throw new DuplicateEmailException(email);
        
        var existingCustomer = _customerRepository.GetByEmail(email);
        if (existingCustomer != null)
            throw new DuplicateEmailException(email);
    }
    
    private bool isEmailValid(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;
        try
        {
            var emailAddress = new MailAddress(email);
            if(emailAddress.Address != email)
                return false;
            return CheckDomainHasMXRecord(emailAddress.Host);
        }
        catch (Exception e)
        {
            return false;
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