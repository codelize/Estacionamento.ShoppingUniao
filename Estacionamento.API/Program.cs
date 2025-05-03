using Estacionamento.Application.Services;
using Estacionamento.Infrastructure.Configurations;
using Estacionamento.Infrastructure.Persistence.Repositories;
using FastEndpoints;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// FastEndpoints
builder.Services.AddFastEndpoints();

// MongoDB settings
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration["MongoSettings:ConnectionString"]));

// Adiciona o IMongoDatabase
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var dbName = builder.Configuration["MongoSettings:DatabaseName"];
    return client.GetDatabase(dbName);
});

// Repositórios
builder.Services.AddScoped<VagaRepository>();
builder.Services.AddScoped<LojaRepository>();

// Serviços de aplicação
builder.Services.AddScoped<RotaService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseFastEndpoints();

app.Run();
