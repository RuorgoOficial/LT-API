using LT.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.core.RabbitMQSender
{
    public interface IRabbitMQMessageSender<T>
        where T : EntityBaseDto
    {
        Task SendMessageAsync(T message, string queueName, CancellationToken cancellationToken);
    }
}
