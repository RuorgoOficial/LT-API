using LT.core.RabbitMQSender;
using LT.model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.Integration.Test.Mocks
{
    public class RabbitMQMessageSenderMock : IRabbitMQMessageSender
    {

        public RabbitMQMessageSenderMock(IConfiguration configuration)
        {
        }

        public void SendMessage(EntityBaseDto message, string? queueName)
        {
            
        }
    }
}
