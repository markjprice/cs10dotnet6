using GraphQL.Server; // GraphQLOptions
using Northwind.GraphQL; // GreetQuery, NorthwindSchema, CategoryType, NorthwindQuery
using Packt.Shared; // AddNorthwindContext extension method

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:5005/");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddNorthwindContext();

builder.Services.AddScoped<NorthwindSchema>();

builder.Services.AddGraphQL()
  .AddGraphTypes(typeof(NorthwindSchema), ServiceLifetime.Scoped)
  .AddDataLoader()
  .AddSystemTextJson(); // serialize responses as JSON

var app = builder.Build();

// Configure the HTTP request pipeline.

if (builder.Environment.IsDevelopment())
{
  app.UseGraphQLPlayground(); // default path is /ui/playground
}

app.UseGraphQL<NorthwindSchema>(); // default path is /graphql

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
