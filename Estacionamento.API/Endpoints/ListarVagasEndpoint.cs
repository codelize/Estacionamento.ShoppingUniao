using FastEndpoints;
using Estacionamento.Infrastructure.Persistence.Repositories;
using Estacionamento.Domain.Entities;

namespace Estacionamento.API.Endpoints;

public class ListarVagasEndpoint : EndpointWithoutRequest<List<Vaga>>
{
    private readonly VagaRepository _vagaRepository;

    public ListarVagasEndpoint(VagaRepository vagaRepository)
    {
        _vagaRepository = vagaRepository;
    }

    public override void Configure()
    {
        Get("/vagas");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var vagas = await _vagaRepository.ListarTodasAsync(ct);

        if (vagas is null || vagas.Count == 0)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(vagas, cancellation: ct);
    }
}
