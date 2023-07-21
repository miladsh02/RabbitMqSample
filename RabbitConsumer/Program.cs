
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

        var factory = new ConnectionFactory
        {
            HostName="localhost",
            UserName="admin",
            Password="admin",
            VirtualHost="/"
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare("MessageQueue", durable: true, exclusive: false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        };

        //channel.BasicConsume(queue: "MessageQueue", autoAck: true, consumer: consumer);

        Console.WriteLine("Press any key to exit.");
        connection.Close();
    