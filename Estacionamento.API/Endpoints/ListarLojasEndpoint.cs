using FastEndpoints;
using Estacionamento.Infrastructure.Persistence.Repositories;
using Estacionamento.Domain.Entities;

namespace Estacionamento.API.Endpoints;

public class ListarLojasEndpoint : EndpointWithoutRequest<List<Loja>>
{
    private readonly LojaRepository _lojaRepository;

    public ListarLojasEndpoint(LojaRepository lojaRepository)
    {
        _lojaRepository = lojaRepository;
    }

    public override void Configure()
    {
        Get("/lojas");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var lojas = await _lojaRepository.ListarTodasAsync(ct);
        await SendAsync(lojas);
    }
}
