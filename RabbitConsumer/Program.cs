
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory=new ConnectionFactory()
            {
                 HostName="localhost",
                 UserName="admin",
                 Password="admin",
                 VirtualHost="/"
            };

            var connection=factory.CreateConnection();

            using var channel=connection.CreateModel();
            channel.QueueDeclare("MessageQueue",durable:true,exclusive:false);
            var consumer=new EventingBasicConsumer(channel);
            consumer.Received+=(model,eventArgs)=>
            {
                //binnary message
                var body=eventArgs.Body.ToArray();
                var message=Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

Console.ReadKey();
