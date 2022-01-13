namespace Core.RabbitMQ.Library.Services
{
    public interface IRabbitMqInitializer
    {
        public void InitializeRabbitMq();
        public void Dispose();

    }
}
