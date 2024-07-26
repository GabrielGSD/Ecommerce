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
	}
}
