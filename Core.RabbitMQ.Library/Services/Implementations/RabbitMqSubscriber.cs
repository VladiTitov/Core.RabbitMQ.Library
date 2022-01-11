using Core.RabbitMQ.Library.Common.ConfigsModels;
using Core.RabbitMQ.Library.Context;
using Core.RabbitMQ.Library.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.RabbitMQ.Library.Services.Implementations
{
    public class RabbitMqSubscriber : IRabbitMqSubscriber, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeType;
        private readonly string _exchangeName;
        private readonly string _routingKey;
        private readonly string _queueName;
        private readonly IEventProcessor _eventProcessor;

        public RabbitMqSubscriber(IRabbitMqContext context, 
            RabbitMqConfiguration configuration,
            IEventProcessor eventProcessor)
        {
            _connection = context.SubscriberConnection;
            _channel = _connection.CreateModel();
            _exchangeType = configuration.ExchangeType;
            _exchangeName = configuration.ExchangeName;
            _routingKey = configuration.RoutingKey;
            _queueName = configuration.QueueName;

            _eventProcessor = eventProcessor;

            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            _channel.ExchangeDeclare(exchange: _exchangeName, type: _exchangeType);
            _channel.QueueDeclare(queue: _queueName);
            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: _routingKey);

            Console.WriteLine("Listening on the message bus");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("RabbitMQ connection shutdown");
        }

        public Task Execute()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, e) =>
            {
                Console.WriteLine("Event Received");

                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(message);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}
