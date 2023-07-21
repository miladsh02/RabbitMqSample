using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMqSample.Models;
using WebApi.Services;

namespace RabbitMqSample.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageProducer _messageProducer;
    private readonly MessageReceiver _messageReceiver;
    public MessageController(IMessageProducer messageProducer)
    {
        _messageProducer=messageProducer;
    }

    [HttpPost("SendMessageToQueue")]
    public IActionResult SendMessageToQueue()
    {
        var randomMessage= GenerateRandomMessage();
        _messageProducer.Publish(randomMessage);

        return Ok();
    }

    [HttpGet("GetMessageFromQueue")]
    public IActionResult GetMessageFromQueue()
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
                return Ok(message);
            }
            connection.Close();
            return NotFound();

    }

    private static MessageModel GenerateRandomMessage()
    {
        return new MessageModel{
            Body = Guid.NewGuid().ToString(),
            Id= Guid.NewGuid(),
            CreationalDate=DateTime.Now
        };
    }
}
