using LT.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.messageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(EntityBase message, string topicName);
    }
}
