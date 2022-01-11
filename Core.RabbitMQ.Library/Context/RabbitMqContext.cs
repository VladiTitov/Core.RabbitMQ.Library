using Core.RabbitMQ.Library.Common.ConfigsModels;
using Core.RabbitMQ.Library.Common.ConfigsModels.Base;
using RabbitMQ.Client;

namespace Core.RabbitMQ.Library.Context
{
    public class RabbitMqContext : IRabbitMqContext
    {
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;

        public IConnection SubscriberConnection { get; }

        public RabbitMqContext(RabbitMqConfiguration rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;

            SubscriberConnection = CreateNewRabbitConnection(_rabbitMqConfiguration.UserName,
                _rabbitMqConfiguration.Password,
                _rabbitMqConfiguration.Port,
                _rabbitMqConfiguration.Hostname);
        }

        public IConnection CreateNewRabbitConnection(
            string userName, 
            string password, 
            int port, 
            string hostName)
        {
            var factory = new ConnectionFactory()
            {
                UserName = userName,
                Password = password,
                Port = port,
                HostName = hostName
            };

            return factory.CreateConnection();
        }
    }
}
