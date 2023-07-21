
using System.Text;
using RabbitMQ.Client;

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
    Console.WriteLine(message);
}
connection.Close();
Console.WriteLine("press a button to exit");  