using FastEndpoints;
using Estacionamento.Infrastructure.Persistence.Repositories;
using Estacionamento.Domain.Entities;

namespace Estacionamento.API.Endpoints;

public class ListarVagasDisponiveisEndpoint : EndpointWithoutRequest<List<Vaga>>
{
    private readonly VagaRepository _vagaRepository;

    public ListarVagasDisponiveisEndpoint(VagaRepository vagaRepository)
    {
        _vagaRepository = vagaRepository;
    }

    public override void Configure()
    {
        Get("/vagas/disponiveis");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var vagasDisponiveis = await _vagaRepository.ListarDisponiveisAsync(ct);
        await SendAsync(vagasDisponiveis);
    }
}
