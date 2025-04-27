using FastEndpoints;
using Estacionamento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Estacionamento.API.Endpoints;
public class OcuparVagaEndpoint : EndpointWithoutRequest
{
    private readonly EstacionamentoDbContext _context;

    public OcuparVagaEndpoint(EstacionamentoDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("/vagas/{id}/ocupar"); // Método PUT para ocupar vaga
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");

        var vaga = await _context.Vagas.FirstOrDefaultAsync(v => v.Id == id, ct);

        if (vaga is null)
        {
            await SendNotFoundAsync();
            return;
        }

        vaga.Ocupa(); // método da entidade Vaga que muda Disponível para false
        await _context.SaveChangesAsync(ct);

        await SendNoContentAsync(); // Resposta 204 No Context
    }
}

