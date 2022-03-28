namespace luxclusif.order.application.Interfaces
{
    public interface IMessageSenderInterface
    {
        Task Send(string name, object data);
    }
}
