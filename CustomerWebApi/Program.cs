using CustomerWebApi;
using JwtAuthServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Added DI for JWTAuthServer after adding to project reference
builder.Services.AddCustomJwtAuth();


/* Database Context Dependency Injection */
var dbHost =Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}"; // for docker
//var connectionString = $"Server={dbHost};Database={dbName};Trusted_Connection=True";  // for my local
builder.Services.AddDbContext<CustomerDbContext>(option => option.UseSqlServer(connectionString));
/* Database Context Dependency Injection */

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();  //Add this 
app.UseAuthorization();

app.MapControllers();

app.Run();
