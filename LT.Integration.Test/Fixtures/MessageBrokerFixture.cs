using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.Integration.Test.Fixtures
{
    public class MessageBrokerFixture : IAsyncLifetime
    {

        public Task DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
