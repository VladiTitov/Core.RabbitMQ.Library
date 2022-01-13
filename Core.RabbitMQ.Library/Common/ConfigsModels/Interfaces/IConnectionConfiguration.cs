namespace Core.RabbitMQ.Library.Common.ConfigsModels.Interfaces
{
    public interface IConnectionConfiguration
    {
        string Hostname { get; set; }
        int Port { get; set; }
        string VirtualHost { get; set; } 
        string UserName { get; set; }
        string Password { get; set; }
    }
}
