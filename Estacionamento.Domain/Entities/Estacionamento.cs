using Estacionamento.Domain.Entities;

namespace Estacionamento.Domain.Entities;

public class Estacionamento
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public List<string> Setores { get; private set; }
    public List<Vaga> Vagas { get; private set; }
    public List<Loja> Lojas { get; private set; }

    public Estacionamento(string nome, List<string> setores)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Setores = setores ?? new List<string>();
        Vagas = new List<Vaga>();
        Lojas = new List<Loja>();
    }

    public void AdicionarVaga(Vaga vaga)
    {
        if (vaga != null)
            Vagas.Add(vaga);
    }

    public void AdicionarLoja(Loja loja)
    {
        if (loja != null)
        Lojas.Add(loja);
    }
}
