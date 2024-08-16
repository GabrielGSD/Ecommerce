namespace Ecommerce.Infrastructure.CrossCutting.Extensions.Ioc
{
	public static class ServicesCollectionExtensions
	{
		public static IServiceCollection AddRavenDb(this IServiceCollection servicesCollection)
		{
			servicesCollection.TryAddSingleton<IDocumentStore>(ctx => {
				var ravenDbSettings = ctx.GetRequiredService<IOptions<RavenDbSettings>>().Value;
				var store = new DocumentStore
				{
					Urls = new[] { ravenDbSettings.Url },
					Database = ravenDbSettings.Database
				};

				store.Initialize();
				return store;
			});

			return servicesCollection;
		}
		
		public static IServiceCollection AddRepositories(this IServiceCollection servicesCollection)
		{
			servicesCollection.TryAddSingleton<ICustomerRepository, CustomerRepository>();
			return servicesCollection;
		}
		
		public static IServiceCollection AddDomainServices(this IServiceCollection servicesCollection)
		{
			servicesCollection.TryAddScoped<ICustomerService, CustomerService>();
			return servicesCollection;
		}
		
		public static IServiceCollection AddMappers(this IServiceCollection servicesCollection)
		{
			servicesCollection.TryAddScoped<IMapper<Customer, CustomerDto>, CustomerMapper>();
			servicesCollection.TryAddScoped<IMapper<CustomerDto, Customer>, CustomerMapper>();
			return servicesCollection;
		}
		
		public static IServiceCollection AddApplicationServices(this IServiceCollection servicesCollection)
		{
			servicesCollection.TryAddScoped<ICustomerApplicationService, CustomerApplicationService>();
			return servicesCollection;
		}
	}
}
