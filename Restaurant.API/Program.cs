using Restaurants.Infrastructure.Extensions;
using Restaurants.Application.Extensions;
    using Restaurants.Infrastructure.Seeders;

using Restaurants.API.Middlewares;
using Restaurants.Domain.Entities;
using Restaurants.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.AddPresentation();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();


var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

await seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddle>();
app.UseMiddleware<RequestTimeLoggerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("api/identity").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
