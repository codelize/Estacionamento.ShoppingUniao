namespace Estacionamento.API.Endpoints.Responses
{
    public class InstrucaoDeRotaResponse
    {
        public string VagaNumero { get; set; }
        public string Setor { get; set; }
        public string Coordenadas { get; set; }
        public List<string> Instrucoes { get; set; }

        public InstrucaoDeRotaResponse()
        {
            Instrucoes = new List<string>();
        }
    }
}
