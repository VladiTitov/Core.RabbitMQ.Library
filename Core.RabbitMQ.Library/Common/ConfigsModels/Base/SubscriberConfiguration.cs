using Core.RabbitMQ.Library.Common.ConfigsModels.Interfaces;

namespace Core.RabbitMQ.Library.Common.ConfigsModels.Base
{
    public class SubscriberConfiguration : IRabbitMqConfiguration
    {
        public string ExchangeType { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
    }
}