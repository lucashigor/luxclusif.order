using luxclusif.order.application.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace luxclusif.order.infrastructure.rabbitmq
{
    public class SendMessageRabbitmq : IMessageSenderInterface
    {
        private const string UName = "guest";
        private const string PWD = "guest";
        private const string HName = "luxclusif-order-rabbitmq";

        public Task Send(string name, object data)
        {
            var connectionFactory = new ConnectionFactory()
            {
                UserName = UName,
                Password = PWD,
                HostName = HName
            };

            var connection = connectionFactory.CreateConnection();

            var model = connection.CreateModel();

            var properties = model.CreateBasicProperties();

            properties.Persistent = false;

            byte[] messagebuffer = Encoding.Default.GetBytes(JsonConvert.SerializeObject(data));

            model.BasicPublish(name, "", properties, messagebuffer);

            return Task.CompletedTask;

        }
    }
}
