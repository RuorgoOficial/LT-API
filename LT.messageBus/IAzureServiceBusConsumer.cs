﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.messageBus
{
    public interface IAzureServiceBusConsumer
    {
        public Task Start();
        public Task Stop();
    }
}
