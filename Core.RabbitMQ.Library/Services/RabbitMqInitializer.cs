using System;
using RabbitMQ.Client;
using Core.RabbitMQ.Library.Common.ConfigsModels.Interfaces;
using Core.RabbitMQ.Library.Context;

namespace Core.RabbitMQ.Library.Services
{
    public class RabbitMqInitializer : IRabbitMqInitializer
    {
        private readonly IConnection _connection;
        public readonly IModel Channel;
        public readonly string ExchangeType;
        public readonly string ExchangeName;
        public readonly string RoutingKey;
        public readonly string QueueName;

        public RabbitMqInitializer(IRabbitMqContext context, IRabbitMqConfiguration configuration)
        {
            _connection = context.Connection;
            Channel = _connection.CreateModel();
            ExchangeType = configuration.ExchangeType;
            ExchangeName = configuration.ExchangeName;
            RoutingKey = configuration.RoutingKey;
            QueueName = configuration.QueueName;

            //InitializeRabbitMq();
        }

        public void InitializeRabbitMq()
        {
            Channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType);
            Channel.QueueDeclare(queue: QueueName);
            Channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RoutingKey);

            Console.WriteLine("Listening on the message bus");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("RabbitMQ connection shutdown");
        }

        public void Dispose()
        {
            if (Channel.IsOpen)
            {
                Channel.Close();
                _connection.Close();
            }
        }
    }
}
