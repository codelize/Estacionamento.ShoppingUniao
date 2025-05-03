using Estacionamento.Domain.Entities;
using Estacionamento.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Estacionamento.Infrastructure.Persistence.Repositories;

public class VagaRepository
{
    private readonly IMongoCollection<Vaga> _collection;

    public VagaRepository(IOptions<MongoSettings> settings, IMongoClient client)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<Vaga>("Vagas");
    }

    public async Task<Vaga?> BuscarPorIdAsync(Guid id, CancellationToken ct) =>
        await _collection.Find(v => v.Id == id).FirstOrDefaultAsync(ct);

    public async Task AtualizarAsync(Vaga vaga, CancellationToken ct) =>
        await _collection.ReplaceOneAsync(v => v.Id == vaga.Id, vaga, cancellationToken: ct);

    public async Task<List<Vaga>> ListarDisponiveisAsync(CancellationToken ct) =>
        await _collection.Find(v => v.Disponivel).ToListAsync(ct);

    public async Task<List<Vaga>> ListarTodasAsync(CancellationToken ct) =>
    await _collection.Find(_ => true).ToListAsync(ct);


}
