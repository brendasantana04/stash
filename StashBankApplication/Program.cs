using StashBankApplication.Configurations;
using StashBankApplication.Repository;
using StashBankApplication.Repository.Impl;
using StashBankApplication.Services;
using StashBankApplication.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddScoped<IUserServices, UserServicesImpl>();
builder.Services.AddScoped<IAccountServices, AccountServicesImpl>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
