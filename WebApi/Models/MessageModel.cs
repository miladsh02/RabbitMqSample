namespace RabbitMqSample.Models;

public class MessageModel
{
    public Guid Id{get; set;}
    public string Body {get;set; }=null!;
    public DateTime creationalDate{get;set; }
}