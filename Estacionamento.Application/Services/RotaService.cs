using Estacionamento.Application.Contracts.Responses;
using Estacionamento.Domain.Entities;

namespace Estacionamento.Application.Services;

public class RotaService
{
    public static InstrucaoDeRotaResponse GerarInstrucao(Vaga vaga, Loja loja)
    {
        var distancia = Math.Abs(vaga.CoordenadaX - loja.CoordenadaX) + Math.Abs(vaga.CoordenadaY - loja.CoordenadaY);

        var instrucoes = new List<string>
        {
            $"Siga {distancia * 2} metros no Setor {vaga.Setor}.",
            "Vire à direita no Corredor B.",
            "Sua vaga estará à esquerda."
        };

        return new InstrucaoDeRotaResponse
        {
            VagaNumero = vaga.Numero,
            Setor = vaga.Setor,
            Coordenadas = $"{vaga.CoordenadaX},{vaga.CoordenadaY}",
            Instrucoes = instrucoes
        };
    }
}
