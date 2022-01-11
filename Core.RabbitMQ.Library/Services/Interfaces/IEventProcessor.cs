namespace Core.RabbitMQ.Library.Services.Interfaces
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}
