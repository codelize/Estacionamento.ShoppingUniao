using FastEndpoints;
using MongoDB.Driver;
using Estacionamento.Domain.Entities;

namespace Estacionamento.API.Endpoints;

public class BuscarMelhorVagaEndpoint : EndpointWithoutRequest<Vaga>
{
    private readonly IMongoCollection<Vaga> _vagasCollection;
    private readonly IMongoCollection<Loja> _lojasCollection;

    public BuscarMelhorVagaEndpoint(
        IMongoDatabase database)
    {
        _vagasCollection = database.GetCollection<Vaga>("Vagas");
        _lojasCollection = database.GetCollection<Loja>("Lojas");
    }

    public override void Configure()
    {
        Get("/vagas/melhorvaga");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var lojaNome = Query<string>("loja");

        if (string.IsNullOrWhiteSpace(lojaNome))
        {
            AddError("loja", "O parâmetro 'loja' é obrigatório.");
            await SendErrorsAsync();
            return;
        }

        var loja = await _lojasCollection
            .Find(l => l.Nome.ToLower() == lojaNome.ToLower())
            .FirstOrDefaultAsync(ct);

        if (loja is null)
        {
            await SendNotFoundAsync();
            return;
        }

        var vagasDisponiveis = await _vagasCollection
            .Find(v => v.Disponivel)
            .ToListAsync(ct);

        if (!vagasDisponiveis.Any())
        {
            await SendNotFoundAsync();
            return;
        }

        var melhorVaga = vagasDisponiveis
            .OrderBy(v => Math.Abs(v.CoordenadaX - loja.CoordenadaX) + Math.Abs(v.CoordenadaY - loja.CoordenadaY))
            .FirstOrDefault();

        await SendOkAsync(melhorVaga);
    }
}
