using Estacionamento.Domain.Entities;

namespace Estacionamento.Infrastructure.Persistence;

public static class DbSeeder
{
    public static void Seed(EstacionamentoDbContext context)
    {
        if (!context.Lojas.Any())
        {
            var lojas = new List<Loja>
            {
                new Loja("Renner", "Setor Azul", 10, 20),
                new Loja("Kalunga", "Setor Verde", 15, 30),
                new Loja("Riachuelo", "Setor Vermelho", 5, 30),
                new Loja("C&A", "Setor Azul", 8 , 22),
                new Loja("Livraria Saraiva", "Setor Verde", 12 , 28)
            };

            context.Lojas.AddRange(lojas);
        }

        if (!context.Vagas.Any())
        {
            var vagas = new List<Vaga>
            {
                new Vaga("A01", "Setor Azul"),
                new Vaga("A02", "Setor Azul"),
                new Vaga("V01", "Setor Verde"),
                new Vaga("V02", "Setor Verde"),
                new Vaga("R01", "Setor Vermelho"),
                new Vaga("R02", "Setor Vermelho")
            };

            context.Vagas.AddRange(vagas);
        }

        context.SaveChanges();
    }
}