using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Estacionamento.Domain.Entities;

public class Vaga
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; private set; }

    public string Numero { get; private set; } = string.Empty;
    public string Setor { get; private set; } = string.Empty;
    public bool Disponivel { get; private set; }
    public int CoordenadaX { get; private set; }
    public int CoordenadaY { get; private set; }

    public Vaga(string numero, string setor, int coordenadaX, int coordenadaY)
    {
        Id = ObjectId.GenerateNewId(); // Corrigido aqui
        Numero = numero;
        Setor = setor;
        Disponivel = true; // Quando criada, a vaga estará livre
        CoordenadaX = coordenadaX;
        CoordenadaY = coordenadaY;
    }

    // Construtor vazio para MongoDB
    public Vaga() { }

    public void Ocupa() => Disponivel = false;

    public void Liberar() => Disponivel = true;
}
