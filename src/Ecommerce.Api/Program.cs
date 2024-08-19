using AspNetCore.Scalar;
using Ecommerce.Infrastructure.CrossCutting.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var ravenDbSettings = builder.Configuration.GetSection("RavenDbSettings");
builder.Services.Configure<RavenDbSettings>(ravenDbSettings);
builder.Services.AddControllers();
builder.Services.AddRavenDb();
builder.Services.AddRepositories();
builder.Services.AddDomainServices();
builder.Services.AddMappers();
builder.Services.AddApplicationServices();
builder.Services.AddHealthChecks()
	.AddRavenDB(setup =>
	{
		setup.Database = ravenDbSettings["Database"];
		setup.Urls = [ravenDbSettings["Url"]];
	});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

	app.UseScalar(options =>
	{
		options.UseTheme(Theme.Default);
		options.RoutePrefix = "docs";
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();