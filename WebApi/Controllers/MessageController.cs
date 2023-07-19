using Microsoft.AspNetCore.Mvc;
using RabbitMqSample.Models;
using WebApi.Services;

namespace RabbitMqSample.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageProducer _messageProducer;
    public static readonly List<MessageModel> _messages=new List<MessageModel>();
    public MessageController(IMessageProducer messageProducer)
    {
        _messageProducer=messageProducer;
    }

    [HttpPost]
    public IActionResult SendMessageToQueue(MessageModel message)
    {
        if(message is null)
        return BadRequest("The message is null");

        _messages.Add(message);
        
        _messageProducer.Publish(message);

        return Ok();
    }

}
