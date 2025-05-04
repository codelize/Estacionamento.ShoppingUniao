using FastEndpoints;
using Estacionamento.Infrastructure.Persistence.Repositories;
using Estacionamento.Domain.Entities;
using MongoDB.Bson;

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
        var idString = Route<string>("id");

        if (!ObjectId.TryParse(idString, out var objectId))
        {
            AddError("id", "Formato de ID inválido.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var vaga = await _vagaRepository.BuscarPorIdAsync(objectId, ct);

        if (vaga is null)
        {
            await SendNotFoundAsync();
            return;
        }

        vaga.Ocupa();
        await _vagaRepository.AtualizarAsync(vaga, ct);

        await SendNoContentAsync();
    }
}
