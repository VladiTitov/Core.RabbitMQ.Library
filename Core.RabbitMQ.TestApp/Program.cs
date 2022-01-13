using System;
using System.Collections.Generic;
using Core.RabbitMQ.Library.Common.ConfigsModels;
using Core.RabbitMQ.Library.Context;
using Core.RabbitMQ.Library.Services.Implementations;
using Core.RabbitMQ.Library.Common.ConfigsModels.Base;
using Core.RabbitMQ.Library.Common.ConfigsModels.Interfaces;
using Microsoft.VisualBasic;

namespace Core.RabbitMQ.TestApp
{
    class Program
    {
        private static ConnectionConfiguration _connectionConfiguration;

        static void Main(string[] args)
        {
            _connectionConfiguration = new ConnectionConfiguration
            {
                Hostname = "localhost",
                Port = 5672,
                VirtualHost = "/",
                UserName = "guest",
                Password = "guest"
            };

            IRabbitMqConfiguration configurationSender = new SubscriberConfiguration
            {
                ExchangeType = "fanout",
                ExchangeName = "parser.exchange",
                QueueName = "parser-queue",
                RoutingKey = "routingKey"
            };

            ICollection<IRabbitMqConfiguration> configurationCollection = new List<IRabbitMqConfiguration>()
            {
                configurationSender,
                new SubscriberConfiguration
                {
                    ExchangeType = "fanout",
                    ExchangeName = "loader.exchange",
                    QueueName = "loader-queue",
                    RoutingKey = "routingKey"
                },
                new SubscriberConfiguration
                {
                    ExchangeType = "direct",
                    ExchangeName = "vasily.exchange",
                    QueueName = "vasily-queue",
                    RoutingKey = "vasily"
                }
            };

            StartConsume(configurationCollection);

            Console.WriteLine("Enter message");

            SendMessage(configurationSender, Console.ReadLine());

            Console.WriteLine("End");
            Console.ReadLine();
        }

        static void StartConsume(IEnumerable<IRabbitMqConfiguration> configurations)
        {
            var rabbitMqContext = new RabbitMqContext(_connectionConfiguration);
            var eventProcessor = new EventProcessor();

            foreach (var configuration in configurations)
            {
                new RabbitMqSubscriber(rabbitMqContext, configuration, eventProcessor).Execute();
            }
        }

        static void SendMessage(IRabbitMqConfiguration configuration, string message)
        {
            var rabbitMqContext = new RabbitMqContext(_connectionConfiguration);

            var rabbitMqPublisher = new RabbitMqPublisher(rabbitMqContext, configuration);

            rabbitMqPublisher.SendMessage(message);
        }
    }
}
