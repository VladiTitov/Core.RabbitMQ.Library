using System;
using Core.RabbitMQ.Library.Services.Interfaces;

namespace Core.RabbitMQ.Library.Services.Implementations
{
    public class EventProcessor : IEventProcessor
    {
        public void ProcessEvent(string message)
        {
            Console.WriteLine(message);
        }
    }
}
