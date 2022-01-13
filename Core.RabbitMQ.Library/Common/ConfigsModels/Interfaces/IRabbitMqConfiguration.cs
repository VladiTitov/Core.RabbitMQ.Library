namespace Core.RabbitMQ.Library.Common.ConfigsModels.Interfaces
{
    public interface IRabbitMqConfiguration
    { 
        string ExchangeType { get; set; }
        string ExchangeName { get; set; }
        string QueueName { get; set; }
        string RoutingKey { get; set; }
    }
}
