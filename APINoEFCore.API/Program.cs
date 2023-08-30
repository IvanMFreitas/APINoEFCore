using System.Data;
using System.Data.SqlClient;
using System.Text;
using APINoEFCore.Data.Repositories;
using APINoEFCore.Data.Repositories.Interface;
using APINoEFCore.Entities.Models;
using APINoEFCore.Services;
using APINoEFCore.Services.Interface;
using APINoEFCore.Services.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var configValue = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

//Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddScoped<IDbConnection>(sp => 
{
    var connection = new SqlConnection(configValue);
    connection.Open();
    return connection;
});

builder.Services.AddScoped<IRepository<Person>, Repository<Person>>();
builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();
builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddAutoMapper(typeof(OrderProfile));
builder.Services.AddAutoMapper(typeof(PersonProfile));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "APINoEFCore",
            ValidAudience = "APINoEFCore",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9uUemxx8pb3i0Kw3ovb4V1k7bMuyAA9h")),
            ClockSkew = TimeSpan.FromMinutes(5)
        };
    });

// Configure authorization policies
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
