namespace Estacionamento.Infrastructure.Configurations;

public class MongoSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string VagasCollectionName { get; set; }
    public string LojasCollectionName { get; set; }
}
