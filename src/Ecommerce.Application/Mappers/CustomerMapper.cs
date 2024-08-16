using Ecommerce.Application.Dtos;
using Ecommerce.Application.Mappers.Interfaces;
using Ecommerce.Domain.Model;

namespace Ecommerce.Application.Mappers;

public class CustomerMapper : IMapper<Customer, CustomerDto>, IMapper<CustomerDto, Customer>
{
    public CustomerDto Map(Customer source)
    {
        return new CustomerDto
        {
            Name = source.Name,
            LastName = source.LastName,
            Email = source.Email,
            BirthDate = source.BirthDate,
            Cpf = source.Cpf,
            Address = new AddressDto
            {
                Street = source.Address.Street,
                Number = source.Address.Number,
                Complement = source.Address.Complement,
                City = source.Address.City,
                State = source.Address.State,
                PostalCode = source.Address.PostalCode
            }
        };
    }

    public IEnumerable<CustomerDto> Map(IEnumerable<Customer> source)
    {
        return source.Select(Map);
    }

    public Customer Map(CustomerDto source)
    {
        return new Customer
        {
            Name = source.Name,
            LastName = source.LastName,
            Email = source.Email,
            BirthDate = source.BirthDate,
            Cpf = source.Cpf,
            Address = new Address
            {
                Street = source.Address.Street,
                Number = source.Address.Number,
                Complement = source.Address.Complement,
                City = source.Address.City,
                State = source.Address.State,
                PostalCode = source.Address.PostalCode
            }
        };
    }

    public IEnumerable<Customer> Map(IEnumerable<CustomerDto> source)
    {
        return source.Select(Map);
    }
}