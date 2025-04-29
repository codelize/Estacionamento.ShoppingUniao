using FastEndpoints;
using Estacionamento.Infrastructure.Persistence;
using Estacionamento.API.Endpoints.Responses;
using Microsoft.EntityFrameworkCore;
using Estacionamento.Domain.Entities;

namespace Estacionamento.API.Endpoints
{
    public class GerarInstrucaoDeRotaEndpoint : EndpointWithoutRequest<InstrucaoDeRotaResponse>
    {
        private readonly EstacionamentoDbContext _context;

        public GerarInstrucaoDeRotaEndpoint(EstacionamentoDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Get("/rota");
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

            // Gerar instruções simples
            var instrucoes = new List<string>
            {
                $"Siga {Math.Abs(melhorVaga.CoordenadaX - loja.CoordenadaX) * 2} metros no Setor {melhorVaga.Setor}.",
                "Vire à direita no Corredor B.",
                "Sua vaga estará à esquerda."
            };

            var response = new InstrucaoDeRotaResponse
            {
                VagaNumero = melhorVaga.Numero,
                Setor = melhorVaga.Setor,
                Coordenadas = $"{melhorVaga.CoordenadaX},{melhorVaga.CoordenadaY}",
                Instrucoes = instrucoes
            };

            await SendOkAsync(response);
        }
    }
}
