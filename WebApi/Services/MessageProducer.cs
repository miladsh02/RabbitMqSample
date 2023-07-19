using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace WebApi.Services
{
    public class MessageProducer : IMessageProducer
    {
        public void Publish<T>(T message)
        {
            var factory=new ConnectionFactory()
            {
                 HostName="localhost",
                 UserName="admin",
                 Password="admin",
                 VirtualHost="/"
            };

            var connection=factory.CreateConnection();

            using var channel=connection.CreateModel();
            channel.QueueDeclare("MessageQueue",durable:true,exclusive:true);

            var jsonString=JsonSerializer.Serialize(message);
            var body =Encoding.UTF8.GetBytes(jsonString);
            channel.BasicPublish("","MessageQueue",body:body);
            
            throw new NotImplementedException();
        }
    }
}