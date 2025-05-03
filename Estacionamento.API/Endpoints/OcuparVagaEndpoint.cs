using FastEndpoints;
using Estacionamento.Infrastructure.Persistence.Repositories;
using Estacionamento.Domain.Entities;

namespace Estacionamento.API.Endpoints;

public class OcuparVagaEndpoint : EndpointWithoutRequest
{
    private readonly VagaRepository _vagaRepository;

    public OcuparVagaEndpoint(VagaRepository vagaRepository)
    {
        _vagaRepository = vagaRepository;
    }

    public override void Configure()
    {
        Put("/vagas/{id}/ocupar");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");

        var vaga = await _vagaRepository.BuscarPorIdAsync(id, ct);

        if (vaga is null)
        {
            await SendNotFoundAsync();
            return;
        }

        vaga.Ocupa(); // marca como ocupada
        await _vagaRepository.AtualizarAsync(vaga, ct);

        await SendNoContentAsync();
    }
}
