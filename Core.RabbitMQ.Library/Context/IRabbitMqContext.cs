using RabbitMQ.Client;

namespace Core.RabbitMQ.Library.Context
{
    public interface IRabbitMqContext
    {
        IConnection PublisherConnection { get; }

        IConnection SubscriberConnection { get; }

        IConnection CreateNewRabbitConnection(string userName, string password, int port, string virtualHost, string hostName);
    }
}
