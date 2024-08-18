namespace Ecommerce.Application;

public class CustomerApplicationService : ICustomerApplicationService
{
    private readonly ICustomerService _customerService;
    private readonly IMapper<CustomerDto, Customer> _mapperEntity;
    private readonly IMapper<Customer, CustomerDto> _mapperDto;

    public CustomerApplicationService(ICustomerService customerService, 
                                    IMapper<CustomerDto, Customer> mapperEntity, 
                                    IMapper<Customer, CustomerDto> mapperDto)
    {
        _customerService = customerService;
        _mapperEntity = mapperEntity;
        _mapperDto = mapperDto;
    }

    public void SaveCustomer(CustomerDto customerDto)
    {
        var customer = _mapperEntity.Map(customerDto);
        _customerService.SaveCustomer(customer);
    }

    public void UpdateCustomer(CustomerDto customerDto)
    {
        var customerEntity = _mapperEntity.Map(customerDto);
        _customerService.UpdateCustomer(customerEntity);
    }

    public void DeleteCustomer(string id)
    {
        _customerService.DeleteCustomer(id);
    }

    public IEnumerable<CustomerDto> GetCustomers()
    {
        var customers = _customerService.GetCustomers();
        return customers.Select(c => _mapperDto.Map(c));
    }

    public CustomerDto GetCustomerById(string id)
    {
        var customer = _customerService.GetCustomerById(id);
        return _mapperDto.Map(customer);
    }
}