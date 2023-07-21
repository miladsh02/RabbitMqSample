using System.Text;
using RabbitMQ.Client;

namespace WebApi.Services
{
    public class MessageReceiver :IMessageReceiver
    {

        public string? ReceiveMessage()
        {
            var factory=new ConnectionFactory()
            {
                 HostName="localhost",
                 UserName="admin",
                 Password="admin",
                 VirtualHost="/"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("MessageQueue",durable:true,exclusive: false);

            var data = channel.BasicGet("MessageQueue", autoAck: true);
            if (data != null)
            {
                var message = Encoding.UTF8.GetString(data.Body.ToArray());
                connection.Close();
                return (message);
            }
            connection.Close();
            return default;

        }
    }
}