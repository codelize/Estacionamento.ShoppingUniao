using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Estacionamento.Domain.Entities;

public class Loja
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; private set; }

    public string Nome { get; private set; } = string.Empty;
    public string Setor { get; private set; } = string.Empty;
    public int CoordenadaX { get; private set; }
    public int CoordenadaY { get; private set; }

    // Construtor para criação
    public Loja(string nome, string setor, int coordenadaX, int coordenadaY)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Setor = setor;
        CoordenadaX = coordenadaX;
        CoordenadaY = coordenadaY;
    }

    // Construtor vazio exigido pelo MongoDB Driver para desserialização
    public Loja() { }
}
