using FastEndpoints;
using Estacionamento.Infrastructure.Persistence;
using Estacionamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.API.Endpoints;

    public class ListarVagasDisponiveisEndpoint : EndpointWithoutRequest<List<Vaga>>
    {
        private readonly EstacionamentoDbContext _context;

        public ListarVagasDisponiveisEndpoint(EstacionamentoDbContext context)
        {
             _context = context;    
        }

        public override void Configure()
        {
            Get("/vagas/disponiveis"); // URL nova para listar só vagas livres 
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var vagasDisponiveis = await _context.Vagas
                .Where(v => v.Disponivel) // Só vagas onde Disponível == true
                .ToListAsync(ct);

            await SendAsync(vagasDisponiveis);
        
    }
}
