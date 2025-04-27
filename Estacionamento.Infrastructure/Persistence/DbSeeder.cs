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
                new Vaga("A01", "Setor Azul", 9, 19),
                new Vaga("A02", "Setor Azul", 11, 19),
                new Vaga("V01", "Setor Verde", 14, 29),
                new Vaga("V02", "Setor Verde", 16, 29),
                new Vaga("R01", "Setor Vermelho", 4, 29),
                new Vaga("R02", "Setor Vermelho", 6, 29)
            };

            context.Vagas.AddRange(vagas);
        }

        context.SaveChanges();
    }
}