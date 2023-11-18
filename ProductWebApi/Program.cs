using Microsoft.EntityFrameworkCore;
using ProductWebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//Database  context Dependency Injection //


//----------Database  context Dependency Injection---------- //

//var dbHost = "localhost";
//var dbName = "dms_product";   //for testing locally without Docker
//var dbPassword = "";
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");

var connectionStrings = $"server={dbHost};port=3306;database={dbName};user=root;password={dbPassword}";
builder.Services.AddDbContext<ProductDbContext>(o => o.UseMySQL(connectionStrings));
//---------Database  context Dependency Injection---------- //

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
