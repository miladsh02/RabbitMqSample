namespace WebApi.Services
{
    public interface IMessageProducer
    {
         void Publish<T>(T message);
    }
}