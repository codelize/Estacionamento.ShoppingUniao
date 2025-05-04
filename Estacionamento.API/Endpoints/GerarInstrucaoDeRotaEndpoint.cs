using FastEndpoints;
using MongoDB.Driver;
using Estacionamento.Application.Contracts.Responses;
using Estacionamento.Application.Services;
using Estacionamento.Domain.Entities;

namespace Estacionamento.API.Endpoints;

public class GerarInstrucaoDeRotaEndpoint : EndpointWithoutRequest<InstrucaoDeRotaResponse>
{
    private readonly IMongoCollection<Vaga> _vagasCollection;
    private readonly IMongoCollection<Loja> _lojasCollection;

    public GerarInstrucaoDeRotaEndpoint(IMongoDatabase database)
    {
        _vagasCollection = database.GetCollection<Vaga>("Vagas");
        _lojasCollection = database.GetCollection<Loja>("Lojas");
    }

    public override void Configure()
    {
        Get("/rota");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var lojaNome = Query<string>("loja");

        if (string.IsNullOrWhiteSpace(lojaNome))
        {
            AddError("loja", "O parâmetro 'loja' é obrigatório.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var loja = await _lojasCollection
            .Find(l => l.Nome.ToLower() == lojaNome.ToLower())
            .FirstOrDefaultAsync(ct);

        if (loja is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var vagasDisponiveis = await _vagasCollection
            .Find(v => v.Disponivel)
            .ToListAsync(ct);

        if (vagasDisponiveis.Count == 0)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var melhorVaga = vagasDisponiveis
            .OrderBy(v => Math.Abs(v.CoordenadaX - loja.CoordenadaX) + Math.Abs(v.CoordenadaY - loja.CoordenadaY))
            .FirstOrDefault();

        if (melhorVaga is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = RotaService.GerarInstrucao(melhorVaga, loja);
        await SendOkAsync(response, ct);
    }
}
