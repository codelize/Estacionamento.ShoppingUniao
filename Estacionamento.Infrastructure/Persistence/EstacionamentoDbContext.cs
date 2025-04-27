using Microsoft.EntityFrameworkCore;
using Estacionamento.Domain.Entities;
using System.Collections.Generic;

namespace Estacionamento.Infrastructure.Persistence;

public class EstacionamentoDbContext : DbContext
{
    public EstacionamentoDbContext(DbContextOptions<EstacionamentoDbContext> options)
    : base(options)
    {
    }

    public DbSet<Vaga> Vagas { get; set; }
    public DbSet<Loja> Lojas { get; set; }
    public DbSet<Estacionamento.Domain.Entities.Estacionamento> Estacionamentos { get; set; }
}
