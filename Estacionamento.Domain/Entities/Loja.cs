namespace Estacionamento.Domain.Entities;

public class Loja
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Setor { get; private set; }
    public int CoordenadaX { get; private set; }
    public int CoordenadaY { get; private set; }

    public Loja(string nome, string setor, int coordenadaX, int coordenadaY)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Setor = setor;
        CoordenadaX = coordenadaX;
        CoordenadaY = coordenadaY;
    }
}
