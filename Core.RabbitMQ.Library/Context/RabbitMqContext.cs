using Core.RabbitMQ.Library.Common.ConfigsModels;
using RabbitMQ.Client;

namespace Core.RabbitMQ.Library.Context
{
    public class RabbitMqContext : IRabbitMqContext
    {
        public IConnection Connection { get; }

        public RabbitMqContext(ConnectionConfiguration rabbitMqConfiguration)
        {
            Connection = CreateNewRabbitConnection(rabbitMqConfiguration.UserName,
                rabbitMqConfiguration.Password,
                rabbitMqConfiguration.Port,
                rabbitMqConfiguration.VirtualHost,
                rabbitMqConfiguration.Hostname);
        }

        public IConnection CreateNewRabbitConnection(
            string userName, 
            string password, 
            int port,
            string virtualHost,
            string hostName)
        {
            var factory = new ConnectionFactory()
            {
                UserName = userName,
                Password = password,
                Port = port,
                VirtualHost = virtualHost,
                HostName = hostName
            };

            return factory.CreateConnection();
        }
    }
}
