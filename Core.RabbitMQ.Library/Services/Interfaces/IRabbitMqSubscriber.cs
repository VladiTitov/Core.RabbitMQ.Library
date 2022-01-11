using System.Threading;
using System.Threading.Tasks;

namespace Core.RabbitMQ.Library.Services.Interfaces
{
    public interface IRabbitMqSubscriber
    {
        Task Execute();
    }
}
