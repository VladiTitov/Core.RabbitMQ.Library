using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using Core.RabbitMQ.Library.Context;
using Core.RabbitMQ.Library.Services.Interfaces;
using Core.RabbitMQ.Library.Common.ConfigsModels.Base;

namespace Core.RabbitMQ.Library.Services.Implementations
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;
        private readonly string _exchangeName;
        private readonly string _routingKey;

        public RabbitMqPublisher(IRabbitMqContext context,
            IOptions<PublisherConfiguration> configuration)
        {
            _connection = context.SubscriberConnection;
            _channel = _connection.CreateModel();
            _queueName = configuration.Value.QueueName;
            _exchangeName = configuration.Value.ExchangeName;
            _routingKey = configuration.Value.RoutingKey;
        }

        public void ChannelConsume(string message)
        {
            _channel.QueueDeclare(queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: _routingKey);

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: _exchangeName,
                routingKey: _routingKey,
                basicProperties: null,
                body: body);
        }
    }
}
