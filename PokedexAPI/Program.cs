
using Microsoft.Extensions.Configuration;
using Microsoft.Win32.SafeHandles;
using PokedexAPI.Classes;
using PokedexAPI.Handlers;
using PokedexAPI.Services;
using PokedexAPI.Services.DataRepositories;
using PokedexAPI.Services.DataRepositories.Interfaces;
using PokedexAPI.Services.Interfaces;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Using Dependency injection to define services
ConfigureServices(builder.Services);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.Configure<ExternalUris>(builder.Configuration.GetSection("ExternalUris"));

//Inizialize global exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureServices(IServiceCollection services)
{
    //Used to create a new instance of the service every time we need it
    services.AddTransient<IPokemonDataRepository, PokemonDataRepository>();
    services.AddTransient<IPokemonService, PokemonService>();
}
