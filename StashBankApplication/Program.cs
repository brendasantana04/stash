using Microsoft.EntityFrameworkCore;
using StashBankApplication.Configurations;
using StashBankApplication.Domain.Policies;
using StashBankApplication.Model.Context;
using StashBankApplication.Repository;
using StashBankApplication.Repository.Impl;
using StashBankApplication.Services;
using StashBankApplication.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddEvolveConfiguration(builder.Configuration, builder.Environment);

builder.Services.AddScoped<IUserServices, UserServicesImpl>();
builder.Services.AddScoped<IAccountServices, AccountServicesImpl>();
builder.Services.AddScoped<ITransferServices, TransferServicesImpl>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICardLimitPolicy, CardLimitPolicy>();
builder.Services.AddScoped<ICardService, CardServiceImpl>();
builder.Services.AddScoped<ISavingsBoxServices, SavingsBoxServicesImpl>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
