using Estacionamento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

// Configura��o de servi��es da aplica��o
builder.Services.AddFastEndpoints();

// Configura��o do banco de dados InMemory
builder.Services.AddDbContext<EstacionamentoDbContext>(options =>
    options.UseInMemoryDatabase("EstacionamentoDb"));

var app = builder.Build();  

// Configura��o da pipeline da aplica��o 
app.UseHttpsRedirection();  

app.UseFastEndpoints(); // FastEndPoints deve ser registrado no pipeline

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EstacionamentoDbContext>();
    DbSeeder.Seed(context);
}

app.Run();