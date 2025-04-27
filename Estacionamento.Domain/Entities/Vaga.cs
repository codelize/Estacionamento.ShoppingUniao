namespace Estacionamento.Domain.Entities;

public class Vaga
{
    public Guid Id { get; private set; }
    public string Numero { get; private set; }
    public string Setor { get; private set; }  
    public bool Disponivel { get; private set; }
    public int CoordenadaX { get; private set; }
    public int CoordenadaY { get; private set; }

    public Vaga(string numero, string setor, int coordenadaX, int coordenadaY)
    {
        Id = Guid.NewGuid();
        Numero = numero;
        Setor = setor;
        Disponivel = true; // Quando criada, a vaga estará ´livre
        CoordenadaX = coordenadaX;
        CoordenadaY = coordenadaY;
    }

    public void Ocupa()
    {
        Disponivel = false;
    }

    public void Liberar()
    {
        Disponivel = true;
    }
}

