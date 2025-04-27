using FastEndpoints;

namespace Estacionamento.API.Endpoints;

public class PingEndpoint : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Get("/ping"); // URL do endpoint: GET /ping
        AllowAnonymous(); // Sem autenticação
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync("Pong! API Funcionando.");
    }
}
