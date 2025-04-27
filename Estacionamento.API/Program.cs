using Estacionamento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

// Configuração de servições da aplicação
builder.Services.AddFastEndpoints();

// Configuração do banco de dados InMemory
builder.Services.AddDbContext<EstacionamentoDbContext>(options =>
    options.UseInMemoryDatabase("EstacionamentoDb"));

var app = builder.Build();  

// Configuração da pipeline da aplicação 
app.UseHttpsRedirection();  

app.UseFastEndpoints(); // FastEndPoints deve ser registrado no pipeline

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EstacionamentoDbContext>();
    DbSeeder.Seed(context);
}

app.Run();