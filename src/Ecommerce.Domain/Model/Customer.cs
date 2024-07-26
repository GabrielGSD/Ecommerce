namespace Ecommerce.Domain.Model
{
	public class Customer
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
		public string Cpf { get; set; }
		public bool Active { get; set; }
		public Address Address { get; set; }
	}
}
