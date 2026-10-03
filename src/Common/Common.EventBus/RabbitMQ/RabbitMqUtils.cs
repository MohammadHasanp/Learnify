using RabbitMQ.Client;

namespace Common.EventBus.RabbitMQ;

public class RabbitMqUtils
{
    public static async Task<IConnection> CreateConnection(string hostName, string userName, string password)
    {
        var factory = new ConnectionFactory()
        {
            UserName = userName,
            Password = password,
            HostName = hostName
        };
        return await factory.CreateConnectionAsync();
    }
}