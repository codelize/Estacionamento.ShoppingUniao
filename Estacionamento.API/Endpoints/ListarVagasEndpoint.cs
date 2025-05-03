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
        await SendAsync(vagas);
    }
}
