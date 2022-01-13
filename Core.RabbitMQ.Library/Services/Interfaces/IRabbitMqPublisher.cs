using System.Threading.Tasks;

namespace Core.RabbitMQ.Library.Services.Interfaces
{
    public interface IRabbitMqPublisher
    {
        Task SendMessage(string message);
    }
}
