using FastEndpoints;
using Estacionamento.Infrastructure.Persistence;
using Estacionamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.API.Endpoints
{
    public class ListarLojasEndpoint : EndpointWithoutRequest<List<Loja>>
    {
        private readonly EstacionamentoDbContext _context;

        public ListarLojasEndpoint(EstacionamentoDbContext context)
        {
             _context = context;
        }

        public override void Configure()
        {
            Get("/lojas");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var lojas = await _context.Lojas.ToListAsync(ct);
            await SendAsync(lojas);
        }

    }
}
