namespace Core.RabbitMQ.Library.Services.Interfaces
{
    public interface IRabbitMqPublisher
    {
        void ChannelConsume(string message);
    }
}
