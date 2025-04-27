using FastEndpoints;
using Estacionamento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Estacionamento.Domain.Entities;

namespace Estacionamento.API.Endpoints
{
    public class BuscarMelhorVagaEndpoint : EndpointWithoutRequest<Vaga>
    {
        private readonly EstacionamentoDbContext _context;

        public BuscarMelhorVagaEndpoint(EstacionamentoDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Get("/vagas/melhorvaga");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var lojaNome = Query<string>("loja");

            if (string.IsNullOrWhiteSpace(lojaNome))
            {
                AddError("loja", "O parâmetro 'loja' é obrigatório.");
                await SendErrorsAsync();
                return;
            }

            var loja = await _context.Lojas
                .FirstOrDefaultAsync(l => l.Nome.ToLower() == lojaNome.ToLower(), ct);

            if (loja is null)
            {
                await SendNotFoundAsync();
                return;
            }

            var vagasDisponiveis = await _context.Vagas
                .Where(v => v.Disponivel)
                .ToListAsync(ct);

            if (!vagasDisponiveis.Any())
            {
                await SendNotFoundAsync();
                return;
            }

            var melhorVaga = vagasDisponiveis
                .OrderBy(v => Math.Abs(v.CoordenadaX - loja.CoordenadaX) + Math.Abs(v.CoordenadaY - loja.CoordenadaY))
                .FirstOrDefault();

            if (melhorVaga is null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendOkAsync(melhorVaga);
        }
    }
}
