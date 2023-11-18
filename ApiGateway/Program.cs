using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using JwtAuthServer;

var builder = WebApplication.CreateBuilder(args);

//====Added Configurations====//
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddCustomJwtAuth();
//====Added Configurations====//



var app = builder.Build();
await app.UseOcelot();  //added this middleware


app.UseAuthorization();
app.UseAuthorization();
app.Run();
