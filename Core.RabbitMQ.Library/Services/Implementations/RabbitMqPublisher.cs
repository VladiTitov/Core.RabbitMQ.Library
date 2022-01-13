using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;
using Core.RabbitMQ.Library.Context;
using Core.RabbitMQ.Library.Services.Interfaces;
using Core.RabbitMQ.Library.Common.ConfigsModels.Interfaces;

namespace Core.RabbitMQ.Library.Services.Implementations
{
    public class RabbitMqPublisher : RabbitMqInitializer, IRabbitMqPublisher
    {
        public RabbitMqPublisher(IRabbitMqContext context, IRabbitMqConfiguration configuration) : base(context, configuration) { }

        public Task SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            Channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKey, basicProperties: null, body: body);

            return Task.CompletedTask;
        }

    }
}
