using Estacionamento.Domain.Entities;
using Estacionamento.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Estacionamento.Infrastructure.Persistence.Repositories;

public class LojaRepository
{
    private readonly IMongoCollection<Loja> _collection;

    public LojaRepository(IOptions<MongoSettings> settings, IMongoClient client)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<Loja>("Lojas");
    }

    public async Task<List<Loja>> ListarTodasAsync(CancellationToken ct) =>
        await _collection.Find(FilterDefinition<Loja>.Empty).ToListAsync(ct);

    public async Task<Loja?> BuscarPorNomeAsync(string nome, CancellationToken ct) =>
        await _collection.Find(l => l.Nome.ToLower() == nome.ToLower()).FirstOrDefaultAsync(ct);

    public async Task<Loja?> BuscarPorIdAsync(ObjectId id, CancellationToken ct) =>
        await _collection.Find(l => l.Id == id).FirstOrDefaultAsync(ct);

    public async Task InserirAsync(Loja loja, CancellationToken ct) =>
        await _collection.InsertOneAsync(loja, cancellationToken: ct);
}
