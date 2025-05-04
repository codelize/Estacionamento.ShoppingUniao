using FastEndpoints;
using Estacionamento.Infrastructure.Persistence.Repositories;
using Estacionamento.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Estacionamento.API.Endpoints;

public class LiberarVagaEndpoint : EndpointWithoutRequest
{
    private readonly VagaRepository _vagaRepository;

    public LiberarVagaEndpoint(VagaRepository vagaRepository)
    {
        _vagaRepository = vagaRepository;
    }

    public override void Configure()
    {
        Put("/vagas/{id}/liberar");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var idString = Route<string>("id");

        if (!ObjectId.TryParse(idString, out var objectId))
        {
            AddError("id", "Formato de ID inválido.");
            await SendErrorsAsync(cancellation: ct); // ✅ Aqui estava o problema
            return;
        }

        var vaga = await _vagaRepository.BuscarPorIdAsync(objectId, ct);

        if (vaga is null)
        {
            await SendNotFoundAsync();
            return;
        }

        vaga.Liberar();
        await _vagaRepository.AtualizarAsync(vaga, ct);
        await SendNoContentAsync();
    }
}