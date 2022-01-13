using Core.RabbitMQ.Library.Context;
using Core.RabbitMQ.Library.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;
using Core.RabbitMQ.Library.Common.ConfigsModels.Interfaces;

namespace Core.RabbitMQ.Library.Services.Implementations
{
    public class RabbitMqSubscriber : RabbitMqInitializer, IRabbitMqSubscriber, IDisposable
    {
        private readonly IEventProcessor _eventProcessor;

        public RabbitMqSubscriber(IRabbitMqContext context, IRabbitMqConfiguration configuration, IEventProcessor eventProcessor) : base(context, configuration)
        {
            _eventProcessor = eventProcessor;
            base.InitializeRabbitMq();
        }

        public Task Execute()
        {
            var consumer = new EventingBasicConsumer(Channel);

            consumer.Received += (ModuleHandle, e) =>
            {
                Console.WriteLine("Event Received");

                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(message);
            };

            base.Channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
