using Hangfire;
using luxclusif.order.application.Interfaces;
using luxclusif.order.application.Models;
using MediatR;

namespace luxclusif.order.application.Notifications
{
    public class DefaultNotification : INotificationHandler<DefaultMessageNotification>
    {
        public readonly IMessageSenderInterface message;

        public DefaultNotification(IMessageSenderInterface message)
        {
            this.message = message;
        }

        public Task Handle(DefaultMessageNotification notification, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => message.Send(notification.EventName, notification.Data));

            return Task.CompletedTask;
        }
    }
}
