using luxclusif.order.application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace luxclusif.order.tests.Components.Utils
{
    public class RabbitMqTestHelper : IMessageSenderInterface
    {
        public Dictionary<string, object> list;

        public RabbitMqTestHelper()
        {
            list = new Dictionary<string, object>();
        }

        public Task Send(string name, object data)
        {
            list.Add(name, data);

            return Task.CompletedTask;
        }
    }
}
