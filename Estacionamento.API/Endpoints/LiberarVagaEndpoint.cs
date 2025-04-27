using FastEndpoints;
using Estacionamento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.API.Endpoints;
public class LiberarVagaEndpoint : EndpointWithoutRequest
{
    private readonly EstacionamentoDbContext _context;

    public LiberarVagaEndpoint(EstacionamentoDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("/vagas/{id}/liberar");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id"); // Pegando o id da rota

        var vaga = await _context.Vagas.FirstOrDefaultAsync(v => v.Id == id, ct);

        if (vaga is null)
        {
            await SendNotFoundAsync();
            return;
        }

        vaga.Liberar(); // Método da entidade Vaga para liberar a vaga
        await _context.SaveChangesAsync(ct);

        await SendNoContentAsync();
    }
}

