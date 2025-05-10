// Below two lines are also added as part of the registering services with the DI container
using Microsoft.EntityFrameworkCore;

using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// services such as DBContext must be registered with the dependency injection (DI) container, these container provides service
// to the controllers
// register the UserContext service inside IService Collection
builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseInMemoryDatabase("UsersList")); // using in-memory database

// Learning dependency injection - The AddScoped method registers the service with a scoped lifetime, the lifetime of single
//  request
builder.Services.AddScoped<IMyDependency, MyDependency>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // enables the Swagger middleware for serving generated JSON document using JSON UI
    app.UseSwaggerUi(options => options.DocumentPath = "/openapi/v1.json");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();