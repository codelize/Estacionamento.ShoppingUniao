using FastEndpoints;
using Estacionamento.Infrastructure.Persistence;
using Estacionamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.API.Endpoints;
public class ListarVagasEndpoint : EndpointWithoutRequest<List<Vaga>>
{
    private readonly EstacionamentoDbContext _context;

    public ListarVagasEndpoint(EstacionamentoDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/vagas");
        AllowAnonymous();
    }
        
    public override async Task HandleAsync(CancellationToken ct)
    {
        var vagas = await _context.Vagas.ToListAsync(ct);
        await SendAsync(vagas);
    }
}
