using Core.RabbitMQ.Library.Common.ConfigsModels;
using Core.RabbitMQ.Library.Common.ConfigsModels.Base;
using Core.RabbitMQ.Library.Context;
using Core.RabbitMQ.Library.Services.Implementations;
using Core.RabbitMQ.Library.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace Core.RabbitMQ.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMqConfiguration subscriber = new RabbitMqConfiguration
            {
                Hostname = "localhost",
                Port = 5672,
                VirtualHost = "/",
                ExchangeType = "fanout",
                ExchangeName = "loader.exchange",
                QueueName = "loader-queue",
                RoutingKey = "routingKey",
                UserName = "guest",
                Password = "guest"
            };

            StartConsume(subscriber);

            Console.ReadLine();
        }

        static void StartConsume(RabbitMqConfiguration configuration)
        { 
            RabbitMqContext rabbitMqContext = new RabbitMqContext(configuration);

            EventProcessor eventProcessor = new EventProcessor();


            RabbitMqSubscriber subscriber = new RabbitMqSubscriber(rabbitMqContext, configuration, eventProcessor);

            subscriber.Execute();
        }
    }
}
