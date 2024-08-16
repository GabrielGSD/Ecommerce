namespace Ecommerce.Application;

public class CustomerApplicationService : ICustomerApplicationService
{
    private readonly ICustomerService _customerService;
    private readonly IMapper<CustomerDto, Customer> _mapperEntity;

    public CustomerApplicationService(ICustomerService customerService, IMapper<CustomerDto, Customer> mapperEntity)
    {
        _customerService = customerService;
        _mapperEntity = mapperEntity;
    }

    public void SaveCustomer(CustomerDto customerDto)
    {
        var customer = _mapperEntity.Map(customerDto);
        _customerService.SaveCustomer(customer);
    }
}