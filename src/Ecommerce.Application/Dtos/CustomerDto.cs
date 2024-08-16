namespace Ecommerce.Application.Dtos;

public class CustomerDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string Cpf { get; set; }
    public AddressDto Address { get; set; }
}