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
    private readonly IMessageReceiver _messageReceiver;
    public MessageController(IMessageProducer messageProducer,
                             IMessageReceiver messageReceiver)
    {
        _messageProducer=messageProducer;
        _messageReceiver=messageReceiver;
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
        var message =_messageReceiver.ReceiveMessage();
        return Ok(message);
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
